package instrumentation

import (
	"context"
	"fmt"
	"os"
	"time"

	"github.com/rs/zerolog/log"
	"go.opentelemetry.io/otel"
	"go.opentelemetry.io/otel/exporters/otlp/otlpmetric/otlpmetricgrpc"
	"go.opentelemetry.io/otel/exporters/otlp/otlptrace/otlptracegrpc"
	"go.opentelemetry.io/otel/propagation"
	"go.opentelemetry.io/otel/sdk/metric"
	"go.opentelemetry.io/otel/sdk/resource"
	sdktrace "go.opentelemetry.io/otel/sdk/trace"
	semconv "go.opentelemetry.io/otel/semconv/v1.17.0"
	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials/insecure"
)

type CloseFunc func()

func StartOTEL(ctx context.Context) (CloseFunc, error) {
	res, err := resource.New(ctx,
		resource.WithAttributes(
			// the service name used to display traces in backends
			semconv.ServiceName("payment-api"),
			semconv.ServiceVersion("v1"),
		),
	)
	if err != nil {
		return nil, fmt.Errorf("failed to create resource: %w", err)
	}

	ctx, cancel := context.WithTimeout(ctx, time.Second)
	defer cancel()

	otelExportEndpoint := os.Getenv("OTEL_EXPORTER_OTLP_ENDPOINT")
	if otelExportEndpoint == "" {
		otelExportEndpoint = "localhost:4317"
	}
	conn, err := grpc.DialContext(ctx, otelExportEndpoint,
		// Note the use of insecure transport here. TLS is recommended in production.
		grpc.WithTransportCredentials(insecure.NewCredentials()),
		grpc.WithBlock(),
	)
	if err != nil {
		return nil, fmt.Errorf("failed to create gRPC connection to collector: %w", err)
	}

	meterProvider, err := setupMeterProvider(ctx, conn, res)
	if err != nil {
		return nil, err
	}

	traceProvider, err := setupTraceProvider(ctx, conn, res)
	if err != nil {
		return nil, err
	}

	closeFunc := func() {
		// Handle shutdown properly so nothing leaks.
		if meterErr := meterProvider.Shutdown(ctx ); meterErr != nil {
			log.Error().Msg(meterErr.Error())
		}

		if tpErr := traceProvider.Shutdown(ctx); tpErr != nil {
			log.Error().Err(tpErr)
		}

		if connErr := conn.Close(); connErr != nil {
			log.Error().Err(connErr)
		}
	}

	return closeFunc, nil
}

func setupMeterProvider(ctx context.Context, conn *grpc.ClientConn, res *resource.Resource) (*metric.MeterProvider, error) {
	// Create a meter provider.
	// You can pass this instance directly to your instrumented code if it
	// accepts a MeterProvider instance.
	metricExporter, err := otlpmetricgrpc.New(ctx, otlpmetricgrpc.WithGRPCConn(conn))
	if err != nil {
		return nil, err
	}

	meterProvider := metric.NewMeterProvider(
		metric.WithResource(res),
		metric.WithReader(metric.NewPeriodicReader(metricExporter,
			// Default is 1m. Set to 3s for demonstrative purposes.
			metric.WithInterval(10*time.Second))),
	)

	// Register as global meter provider so that it can be used via otel.Meter
	// and accessed using otel.GetMeterProvider.
	// Most instrumentation libraries use the global meter provider as default.
	// If the global meter provider is not set then a no-op implementation
	// is used, which fails to generate data.
	otel.SetMeterProvider(meterProvider)
	return meterProvider, nil
}

func setupTraceProvider(ctx context.Context, conn *grpc.ClientConn, res *resource.Resource) (*sdktrace.TracerProvider, error) {
	// Set up a trace exporter
	traceExporter, err := otlptracegrpc.New(ctx, otlptracegrpc.WithGRPCConn(conn))
	if err != nil {
		return nil, fmt.Errorf("failed to create trace exporter: %w", err)
	}

	tp := sdktrace.NewTracerProvider(
		sdktrace.WithSampler(sdktrace.AlwaysSample()),
		sdktrace.WithResource(res),
		sdktrace.WithBatcher(traceExporter),
	)
	otel.SetTracerProvider(tp)
	otel.SetTextMapPropagator(propagation.NewCompositeTextMapPropagator(propagation.TraceContext{}, propagation.Baggage{}))
	return tp, nil
}

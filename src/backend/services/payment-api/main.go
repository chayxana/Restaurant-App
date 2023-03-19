package main

import (
	"context"
	"fmt"
	"net"
	"os"
	"strconv"
	"time"

	grpcsvc "github.com/chayxana/payment-api/grpc"
	v1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/rs/zerolog"
	"github.com/rs/zerolog/log"
	"go.opentelemetry.io/contrib/instrumentation/google.golang.org/grpc/otelgrpc"
	"go.opentelemetry.io/otel"
	"go.opentelemetry.io/otel/exporters/otlp/otlptrace/otlptracegrpc"
	"go.opentelemetry.io/otel/propagation"
	"go.opentelemetry.io/otel/sdk/resource"
	sdktrace "go.opentelemetry.io/otel/sdk/trace"
	semconv "go.opentelemetry.io/otel/semconv/v1.17.0"

	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials/insecure"
	"google.golang.org/grpc/reflection"
)

func main() {
	zerolog.TimeFieldFormat = zerolog.TimeFormatUnix

	tp, err := initTracer()
	if err != nil {
		log.Fatal().Err(err)
	}
	defer func() {
		if err := tp.Shutdown(context.Background()); err != nil {
			log.Error().Err(err)
		}
	}()

	lis, err := net.Listen("tcp", ":8080")
	if err != nil {
		log.Fatal().Err(err)
	}

	s := grpc.NewServer(
		grpc.UnaryInterceptor(otelgrpc.UnaryServerInterceptor()),
		grpc.StreamInterceptor(otelgrpc.StreamServerInterceptor()),
	)
	reflection.Register(s)

	enableTestCards, err := getenvBool("ENABLE_TEST_CARDS")
	if err != nil {
		log.Fatal().Err(err)
	}

	v1.RegisterPaymentServiceServer(s, grpcsvc.NewPaymentServiceGrpc(enableTestCards))

	log.Info().Msg("Starting gRPC server on port 8080...")
	if err := s.Serve(lis); err != nil {
		log.Fatal().Err(err)
	}
}

func initTracer() (*sdktrace.TracerProvider, error) {
	ctx := context.Background()

	res, err := resource.New(ctx,
		resource.WithAttributes(
			// the service name used to display traces in backends
			semconv.ServiceName("payment-api"),
		),
	)
	if err != nil {
		return nil, fmt.Errorf("failed to create resource: %w", err)
	}

	// If the OpenTelemetry Collector is running on a local cluster (minikube or
	// microk8s), it should be accessible through the NodePort service at the
	// `localhost:30080` endpoint. Otherwise, replace `localhost` with the
	// endpoint of your cluster. If you run the app inside k8s, then you can
	// probably connect directly to the service through dns.
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

func getenvBool(key string) (bool, error) {
	s, ok := os.LookupEnv(key)
	if !ok {
		return false, nil
	}
	v, err := strconv.ParseBool(s)
	if err != nil {
		return false, err
	}
	return v, nil
}

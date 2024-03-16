package main

import (
	"context"
	"net"
	"net/http"
	"os"
	"strconv"

	grpcsvc "github.com/chayxana/payment-api/grpc"
	"github.com/chayxana/payment-api/handlers"
	"github.com/chayxana/payment-api/instrumentation"
	v1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/rs/zerolog"
	"github.com/rs/zerolog/log"
	"go.opentelemetry.io/contrib/instrumentation/google.golang.org/grpc/otelgrpc"
	"go.opentelemetry.io/contrib/instrumentation/net/http/otelhttp"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"
)

func main() {
	zerolog.TimeFieldFormat = zerolog.TimeFormatUnix

	ctx := context.Background()

	closeFunc, err := instrumentation.StartOTEL(ctx)
	if err != nil {
		log.Fatal().Err(err)
	}
	defer closeFunc()

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
	go func() {
		if err := s.Serve(lis); err != nil {
			log.Fatal().Err(err)
		}
	}()

	paymentMethodsHandler := &handlers.PaymentMethodsHandler{}

	mux := http.NewServeMux()
	mux.HandleFunc("/health", func(w http.ResponseWriter, r *http.Request) {
		_, err := w.Write([]byte("healthy"))
		if err != nil {
			log.Error().Err(err)
			return
		}
		w.WriteHeader(http.StatusOK)
	})

	basePath, _ := os.LookupEnv("BASE_PATH")

	mux.HandleFunc(basePath + "/api/v1/paymentMethods", paymentMethodsHandler.GetPaymentMethods)
	mux.HandleFunc(basePath + "/api/v1/paymentMethod/{id}", paymentMethodsHandler.GetPaymentMethod)
	log.Info().Msg("http server listening 8980...")

	otelMux := otelhttp.NewHandler(mux, "server",
		otelhttp.WithMessageEvents(otelhttp.ReadEvents, otelhttp.WriteEvents),
	)

	if err := http.ListenAndServe(":8980", otelMux); err != nil {
		log.Fatal().Err(err)
	}
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

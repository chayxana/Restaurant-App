package main

import (
	"log"
	"net"
	"os"
	"strconv"

	grpcsvc "github.com/chayxana/payment-api/grpc"
	v1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/rs/zerolog"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"
)

func main() {
	zerolog.TimeFieldFormat = zerolog.TimeFormatUnix

	lis, err := net.Listen("tcp", ":8080")
	if err != nil {
		log.Fatalf("Failed to listen: %v", err)
	}

	s := grpc.NewServer()
	reflection.Register(s)

	enableTestCards, err := getenvBool("ENABLE_TEST_CARDS")
	if err != nil {
		log.Fatal(err)
	}

	v1.RegisterPaymentServiceServer(s, grpcsvc.NewPaymentServiceGrpc(enableTestCards))

	log.Println("Starting gRPC server on port 8080...")
	if err := s.Serve(lis); err != nil {
		log.Fatalf("Failed to serve: %v", err)
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

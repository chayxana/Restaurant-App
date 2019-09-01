#!/bin/sh

CGO_ENABLED=0 go test ./controllers ./repositories ./eureka ./oidc --cover -coverprofile=coverage.out | tee output.txt
go tool cover -html=coverage.out -o ./reports/coverage.html
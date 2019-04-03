#!/bin/sh

apk add --no-cache git

CGO_ENABLED=0 go test ./controllers ./repositories ./eureka --cover -coverprofile=coverage.out
go tool cover -html=coverage.out -o ./reports/coverage.html
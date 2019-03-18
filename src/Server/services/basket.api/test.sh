#!/bin/sh

apk add --no-cache git

go test -v ./controllers --cover -coverprofile=coverage.out
go tool cover -html=coverage.out -o coverage.html
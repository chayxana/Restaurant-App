
#!/bin/bash

docker build -t jurabek/dotnet-sdk:2.2-alpine -f dotnet-alpine.Dockerfile .
docker build -t jurabek/golang:1.11-alpine -f go1.11-alpine.Dockerfile .
docker build -t jurabek/openjdk:8-jdk-alpine -f openjdk8-alpine.Dockerfile .

docker push jurabek/dotnet-sdk:2.2-alpine
docker push jurabek/golang:1.11-alpine
docker push jurabek/openjdk:8-jdk-alpine
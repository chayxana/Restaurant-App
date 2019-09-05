FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine

RUN dotnet tool install --global dotnet-sonarscanner --version 4.6.2
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN apk add --no-cache openjdk8-jre
RUN apk add --no-cache curl git jq python py-pip
RUN pip install awscli
RUN apk del curl git py-pip
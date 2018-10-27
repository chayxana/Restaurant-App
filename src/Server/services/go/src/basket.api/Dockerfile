#build stage
FROM golang:alpine AS builder

WORKDIR $GOPATH/src/basket.api
COPY . ./

RUN apk add --no-cache git
RUN go get -d -v ./...
RUN go install -v ./...

#final stage
FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY --from=builder /go/bin/basket.api ./
ENTRYPOINT ./basket.api    
LABEL Name=basket.api Version=0.0.1
EXPOSE 5200
ENV PORT 5200

#build stage
FROM golang:1.22-alpine AS builder

RUN apk add --no-cache git

WORKDIR /src
COPY ./go.mod ./go.sum ./

ENV GOPROXY=https://proxy.golang.org
ENV GOSUMDB=sum.golang.org
ENV GO111MODULE=on
RUN go mod download

COPY ./ ./

RUN CGO_ENABLED=0 go test ./...
RUN CGO_ENABLED=0 go build -installsuffix 'static' -o payment-api main.go

#final stage
FROM alpine:latest
RUN apk --no-cache add ca-certificates

COPY --from=builder /src ./
CMD ["./payment-api"] 
LABEL Name=payment-api Version=0.0.1
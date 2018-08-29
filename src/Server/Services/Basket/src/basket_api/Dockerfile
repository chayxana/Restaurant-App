#build stage
FROM golang:alpine AS builder
WORKDIR /go/src/basket_api
COPY . .
RUN apk add --no-cache git
RUN go get -d -v ./...
RUN go install -v ./...

#final stage
FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY --from=builder /go/bin/basket_api /basket_api
ENTRYPOINT ./basket_api    
LABEL Name=basket_api Version=0.0.1
EXPOSE 5000
ENV PORT 5000

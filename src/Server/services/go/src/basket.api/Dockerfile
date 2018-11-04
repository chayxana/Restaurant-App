#build stage
FROM golang:1.10-alpine AS builder

WORKDIR $GOPATH/src/basket.api
COPY . ./

RUN apk add --no-cache git
RUN go get -d -v -t ./...
RUN go install -v ./...
RUN CGO_ENABLED=0 go test -c ./controllers -o /tests/controllers.test -cover

#final stage
FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY --from=builder /go/bin/basket.api ./
COPY --from=builder /tests/controllers.test ./
CMD ["./basket.api"] 
LABEL Name=basket.api Version=0.0.1
EXPOSE 5200
ENV PORT 5200

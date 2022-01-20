#build stage
FROM golang:1.17-alpine AS builder

RUN apk add --no-cache git

WORKDIR /src
COPY ./go.mod ./go.sum ./

ENV GOPROXY=https://proxy.golang.org
ENV GOSUMDB=sum.golang.org
ENV GO111MODULE=on
RUN go mod download

COPY ./ ./

RUN CGO_ENABLED=0 go build -installsuffix 'static'
RUN CGO_ENABLED=0 go test -c ./handlers -o handlers.test -cover

#final stage
FROM alpine:latest
RUN apk --no-cache add ca-certificates
COPY --from=builder /src ./
CMD ["./basket.api"] 
LABEL Name=basket.api Version=0.0.1
EXPOSE 5200
ENV PORT 5200

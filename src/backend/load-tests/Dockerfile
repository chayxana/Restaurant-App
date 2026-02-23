FROM golang:1.23 as builder

RUN go install go.k6.io/xk6/cmd/xk6@latest

RUN xk6 build \
    --with github.com/szkiba/xk6-faker@v0.3.0 \
    --with github.com/grafana/xk6-output-influxdb \
    --output /k6

FROM grafana/xk6
COPY --from=builder /k6 /usr/bin/k6
ENTRYPOINT [ "k6" ]
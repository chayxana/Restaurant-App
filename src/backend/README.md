# Getting started

### Prerequisites

- Docker Desktop

### How to Run

Use Docker Compose for running. Run following command from the root directory `cd src/backend` and `sh ./docker/run.sh`

### Exposed endpoints

When you are using `docker` services url's will be exposed via `traefik` reverse proxy, it can be accessed via [localhost](http://localhost:8080) default path opens frontend web application

## Grafana

- [Grafana Dashboard](http://localhost:3000)
  - Tempo
  - Loki
  - Prometheus

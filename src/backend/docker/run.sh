docker-compose -f docker/docker-compose.yml \
    -f docker/docker-compose.override.yml \
    -f docker/docker-compose.traefik.yml \
    -f docker/docker-compose.kafka.yml \
    -f docker/docker-compose.grafana.yaml \
    -f docker/docker-compose.otel.yml up -d --build

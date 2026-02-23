#!/bin/sh

set -e

trap close INT

# Ensure host.docker.internal is resolvable from the host
if ! grep -q "host.docker.internal" /etc/hosts; then
  echo "ERROR: host.docker.internal is missing from /etc/hosts."
  echo "Run the following command and retry:"
  echo "  echo '127.0.0.1 host.docker.internal' | sudo tee -a /etc/hosts"
  exit 1
fi

# Run docker-compose with multiple files
docker-compose -f docker/docker-compose.yml -f docker/docker-compose.override.yml \
  -f docker/docker-compose.traefik.yml \
  -f docker/docker-compose.kafka.yml \
  -f docker/docker-compose.grafana.yaml \
  -f docker/docker-compose.otel.yml \
  --project-directory . up -d --build

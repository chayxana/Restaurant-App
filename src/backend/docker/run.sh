#!/bin/sh

set -e

trap close INT

dockerhost="127.0.0.1    host.docker.internal"
hostfile="/etc/hosts"

if [ "$EUID" -ne 0 ]; then
  if ! grep -q "^$dockerhost" "$hostfile"; then
    tput setaf 1
    tput bold
    echo "-----------------------------------------------------------------------------------------------------------"
    echo "Note this script needs sudo permission, to append '127.0.0.1 host.docker.internal' into /etc/hosts"
    echo "-----------------------------------------------------------------------------------------------------------"
    tput sgr 0
    exit
  fi
fi

docker-compose --env-file docker/.env -f docker/docker-compose.yml -f docker/docker-compose.override.yml \
  -f docker/docker-compose.traefik.yml \
  -f docker/docker-compose.kafka.yml \
  -f docker/docker-compose.grafana.yaml \
  -f docker/docker-compose.otel.yml --project-directory . up -d --build

tput bold
tput setaf 3 # yellow

if ! grep -q "^$dockerhost" "$hostfile"; then
  echo "- Adding entry '$dockerhost"
  echo "$dockerhost" >>"$hostfile"
fi

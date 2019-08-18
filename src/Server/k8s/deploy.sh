#!/bin/bash

kubectl delete -f ingress.yml \
               -f services.yml \
               -f deployement.yml \
               -f pgsql-data.yml \
               -f redis-data.yml \
               -f config-map.yml

kubectl apply -f ingress.yml \
               -f services.yml \
               -f deployement.yml \
               -f pgsql-data.yml \
               -f redis-data.yml \
               -f config-map.yml
#!/bin/bash

kubectl delete deployments --all
kubectl delete services --all
kubectl delete configmap local-config || true
kubectl delete -f ingress.yml
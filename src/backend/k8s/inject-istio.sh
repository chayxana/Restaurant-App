#!/bin/bash

# trap ctrl-c and call ctrl_c()
trap close INT

istioctl kube-inject -f deployment.yml | kubectl apply -f -
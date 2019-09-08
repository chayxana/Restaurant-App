#!/bin/bash

echo "Deleting k8s resources..."

kubectl delete deployments --all
kubectl delete services --all
kubectl delete configmap local-config|| true
kubectl delete -f ingress.yml

echo "Deploying infrastructure components..."
kubectl create -f pgsql-data.yml -f redis-data.yml -f config-map.yml

echo "Creating services..."
kubectl create -f services.yml
kubectl create -f ingress.yml

ip_regex='([0-9]{1,3}\.){3}[0-9]{1,3}|[A-B]|[a-b]'
while true; do
    printf "."
    ingressUrl=$(kubectl get svc ingress-nginx -n ingress-nginx -o=jsonpath="{.status.loadBalancer.ingress[0].ip}" | kubectl get svc ingress-nginx -n ingress-nginx -o=jsonpath="{.status.loadBalancer.ingress[0].hostname}")
    if [[ $ingressUrl =~ $ip_regex ]]; then
        break
    fi
    sleep 5s
done

printf "\n"
externalURL=$ingressUrl
echo "Using $externalURL as the external DNS/IP of the K8s cluster"


kubectl create -f deployment.yml

echo "Basket API is exposed at http://$externalURL/basket/swagger/index.html
Order API at http://$externalURL/order/swagger-ui.html 
Menu at http://$externalURL/menu/swagger/index.html
Identity http://$externalURL/identity/swagger/index.html"

echo "Deployment DONE!"
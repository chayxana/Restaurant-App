#!/bin/bash

# trap ctrl-c and call ctrl_c()
trap close INT

if [ "$EUID" -ne 0 ]; then 
  tput setaf 1 
  tput bold 
  echo "-------------------------------------------------------------------------------------------"
  echo "Note this script needs sudo permissions... Please run it again with sudo."
  echo "-------------------------------------------------------------------------------------------"
  tput sgr 0
  exit
fi

echo "Deleting k8s resources..."

kubectl delete deployments --all
kubectl delete services --all
kubectl delete configmap local-config || true
kubectl delete configmap internal-urls || true
kubectl delete configmap endpoints || true
kubectl delete -f ingress.yml

echo "Deploying infrastructure components..."
kubectl create -f pgsql-data.yml -f redis-data.yml -f config-map.yml -f internal-urls.yml

echo "Creating services..."
kubectl create -f services.yml
kubectl create -f ingress.yml

tput bold
tput setaf 2 # yellow
ip_regex='([0-9]{1,3}\.){3}[0-9]{1,3}|[A-B]|[a-b]'
# echo "-------------------------------------------------------------------------------------------"
# echo "Please wait, retrieving ingress DNS/IP"
# while true; do
#     printf "."
#     ingressUrl=$(kubectl get ing restaurant-ingress -o=jsonpath="{.status.loadBalancer.ingress[0].ip}")
#     if [[ $ingressUrl =~ $ip_regex ]]; then
#         break
#     fi
#     sleep 5s
# done

printf "\n"
externalURL=$ingressUrl
echo "Using $externalURL as the external DNS/IP of the K8s cluster"

hostfile="/etc/hosts"
tput bold
tput setaf 3 # yellow

# HOSTS=$(kubectl get ing restaurant-ingress | awk '{print $2}')
# for host in $(echo $HOSTS | tr "," "\n")
# do
#  if ! grep -q "^$externalURL $host" "$hostfile"; then
#     echo "- Adding entry '$externalURL $host"
#     echo "$externalURL $host" >> "$hostfile"
#  fi
# done

tput bold
tput setaf 0 # reset
kubectl create configmap endpoints \
    "--from-literal=basket_pub=http://localhost/basket" \
    "--from-literal=order_pub=http://localhost/order" \
    "--from-literal=menu_pub=http://localhost/menu" \
    "--from-literal=identity_pub=http://localhost/identity" \
    "--from-literal=dashboard_pub=http://localhost/dashboard" \

kubectl create -f deployment.yml
echo "Deployment DONE!"
# Kubernetes Ingress setup locally

## Docker for Mac and Docker Desktop

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/mandatory.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud-generic.yaml
```

## Minikube

```bash
minikube addons enable ingress
```

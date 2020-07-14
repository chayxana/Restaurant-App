# Using k8s locally

Prerequisites

- For masOS
  - Docker Desktop, Version >= 2.0.0
  - Minikube or k8s enabled for Docker Desktop
    - [minikube](https://minikube.sigs.k8s.io/docs/start/)
    - [Docker Desktop k8s](https://rominirani.com/tutorial-getting-started-with-kubernetes-with-docker-on-mac-7f58467203fd)
    - [k8s dashboard](https://github.com/kubernetes/dashboard)

```bash
# Use for getting token to login k8s dashboard
kubectl -n kube-system describe secret $(kubectl -n kube-system get secret | grep admin-user | awk '{print $1}')
```

## How Setup k8s Ingress Controller

### k8s Ingress Docker Desktop

In order to up in running ingress controller, we need deploy first Ingress controller related services to out k8s cluster

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/mandatory.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud-generic.yaml

# To be able to use Identity Server 4 behind the k8s ingress controller
kubect apply -f k8s/docker-setup/ingress-config-map.yml
```

### k8s Ingress Minikube

```bash
minikube addons enable ingress
```

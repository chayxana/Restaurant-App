kustomize build ./crds | kubectl create -f -

# wait all CRD's are available
kustomize build ./crds | kubectl wait --for condition=established --timeout=60s -f -

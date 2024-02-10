kustomize build ./crds | kubectl apply -f -

# wait all CRD's are available
kustomize build ./crds | kubectl wait --for condition=established --timeout=60s -f -
kustomize build . | kubectl apply -f -



docker_build_and_push() {
    echo "Docker build..."
    # docker build --compress -t $IMAGE_BASE_NAME:$CI_API_NAME .
    # docker push $IMAGE_BASE_NAME:$CI_API_NAME
}
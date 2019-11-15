
image_tag=$(date '+%Y%m%d%H%M')
container_registry='jurabek'
push_images='yes'

while [[ $# -gt 0 ]]; do
  case "$1" in
    --skip-image-push )
        push_images=''; shift ;;
    -h | --help )
        usage; exit 1 ;;
    *)
        echo "Unknown option $1"
        usage; exit 2 ;;
  esac
done

echo "#################### Building Docker images ####################"
docker-compose -f ./docker-compose.yml build 

# Remove temporary images
# docker rmi $(docker images -qf "dangling=true")

echo "#################### Tagging images to registry ####################"
services=(identity menu basket order dashboard)

for service in "${services[@]}"
do
    docker tag "restaurant/$service" "$container_registry/$service"
    if [[ $push_images ]]; then
        echo "Pushing image for service $service..."
        docker push "$container_registry/$service"
    fi
done

image_tag=$(date '+%Y%m%d%H%M')
container_registry='jurabek'

echo "#################### Building Docker images ####################"
docker-compose -f ./docker-compose.yml build

# Remove temporary images
# docker rmi $(docker images -qf "dangling=true")

echo "#################### Pushing images to registry ####################"
services=(identity menu basket order)

for service in "${services[@]}"
do
    echo "Pushing image for service $service..."
    docker tag "restaurant/$service" "$container_registry/$service"
    docker push "$container_registry/$service"
done
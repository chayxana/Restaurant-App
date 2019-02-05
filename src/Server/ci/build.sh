#!/bin/sh

CI_API_NAME=$1

main () {
    case "$CI_API_NAME" in
        basket_api) build_basket_api ;;
        order_api) build_order_api ;;
        identity_api) build_identity_api ;;
        menu_api) build_menu_api ;;
        all) 
            build_basket_api
            build_order_api
            build_identity_api
            build_menu_api
            ;;
        *) echo "Invalid api name!" ;;
    esac
}


build_basket_api() {
    cd  ./services/basket.api/
    docker_build_and_push
}

build_order_api(){
    cd ./services/order.api/
    docker_build_and_push
}
build_identity_api() {
    cd ./services/identity.api/
    docker_build_and_push
}

build_menu_api() {
    cd ./services/menu.api/
    docker_build_and_push
}

docker_build_and_push() {
    docker build --compress -t $IMAGE_BASE_NAME:$CI_API_NAME .
    docker push $IMAGE_BASE_NAME:$CI_API_NAME
}

main "$@"
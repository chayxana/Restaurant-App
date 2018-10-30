#!/bin/sh

CI_API_NAME=$1

main () {
    case "$CI_API_NAME" in
        BASKET_API) build_basket_api ;;
        ORDER_API) build_order_api ;;
        IDENTITY_API) build_identity_api ;;
        MENU_API) build_menu_api ;;
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
    cd  ./services/go/src/basket.api/
    docker_build_and_push
}

build_order_api(){
    cd ./services/java/order.api/
    docker_build_and_push
}
build_identity_api() {
    cd ./services/netcore/Identity.API/
    docker_build_and_push
}

build_menu_api() {
    cd ./services/netcore/Menu.API/
    docker_build_and_push
}

docker_build_and_push() {
    docker build --compress --force-rm -t "$IMAGE_BASE_NAME:$CI_API_NAME" .
    docker push "$IMAGE_BASE_NAME:$CI_API_NAME"
}

main "$@"
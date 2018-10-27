#!/bin/sh

API_NAME=$1

main () {
    ls
    case "$API_NAME" in
        basket) build_basket_api ;;
        order) build_order_api ;;
        identity) build_identity_api ;;
        menu) build_menu_api ;;
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
    docker build .
}

build_order_api(){
    cd ./services/java/order.api/
    docker build .
}
build_identity_api() {
    cd ./services/netcore/Identity.API/
    docker build .
}

build_menu_api() {
    cd ./services/netcore/Menu.API/
    docker build .
}

main "$@"
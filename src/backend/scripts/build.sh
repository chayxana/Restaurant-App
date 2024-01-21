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

build_order_api(){
    cd ./services/order-api/
    sh build.sh
    cd -
}

build_basket_api() {
    cd ./services/cart-api/
    echo "#### Building Basket API"
    sh build.sh
    cd -
}

build_identity_api() {
    cd ./services/identity.api/
    sh build.sh
    cd -
}

build_menu_api() {
    cd ./services/menu.api/
    sh build.sh
    cd -
}

main "$@"
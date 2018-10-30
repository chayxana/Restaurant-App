#!/bin/sh

CI_API_NAME=$1

main () {
    case "$CI_API_NAME" in
        BASKET_API) test_basket_api ;;
        ORDER_API) test_order_api ;;
        IDENTITY_API) test_identity_api ;;
        MENU_API) test_menu_api ;;
        all)
            build_basket_api
            build_order_api
            build_identity_api
            build_menu_api
        ;;
        *) echo "Invalid api name!" ;;
    esac
}

test_basket_api() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME ./controllers.test
}

test_order_api(){
    echo "test order api"
}
test_identity_api() {
    echo "test identity api"
}

test_menu_api() {
    echo "test menu api"
}

main "$@"
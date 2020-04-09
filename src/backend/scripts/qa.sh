#!/bin/sh

CI_API_NAME=$1
main () {
    
    case "$CI_API_NAME" in
        basket_api) qa_basket_api ;;
        order_api) qa_order_api ;;
        identity_api) qa_identity_api ;;
        menu_api) qa_menu_api ;;
        all)
            qa_basket_api
            qa_order_api
            qa_identity_api
            qa_menu_api
        ;;
        *) echo "Invalid api name!" ;;
    esac
}

qa_basket_api() {
    cd ./services/basket.api
    sh qa.sh
}

qa_order_api() {
    cd ./services/order.api/
    sh qa.sh
}

qa_menu_api() {
    cd ./services/menu.api/
    sh qa.sh
}

qa_identity_api() {
    cd ./services/identity.api
    sh qa.sh
}

main "$@"
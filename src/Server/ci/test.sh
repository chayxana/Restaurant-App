#!/bin/sh

CI_API_NAME=$1

main () {
    case "$CI_API_NAME" in
        basket_api) test_basket_api ;;
        order_api) test_order_api ;;
        identity_api) test_identity_api ;;
        menu_api) test_menu_api ;;
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
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Identity.API.UnitTests.dll"
}

test_menu_api() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Menu.API.UnitTests.dll"
    
    # code coverage 
    docker run -e GITHUB_USER_NAME="$GITHUB_USER_NAME" \
    -e GITHUB_USER_PASSWORD="$GITHUB_USER_PASSWORD" \
    --name "$CI_API_NAME_coverage" \
    $IMAGE_BASE_NAME:$CI_API_NAME /bin/bash -c ./code_coverage.sh | tee output.txt
    docker rm $(docker ps -aqf "name=$CI_API_NAME_coverage")
    
    COVERAGE_RESULT=$(grep "Total Branch" output.txt | tr -dc '[0-9]+\.[0-9]')
    curl -o badges/menu_coverage.svg https://img.shields.io/badge/coverage-$COVERAGE_RESULT%25-orange.svg
}

main "$@"
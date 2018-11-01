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
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet vstest" "./Identity.API.UnitTests.dll"
}

test_menu_api() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Menu.API.UnitTests.dll"

    docker run --name "$CI_API_NAME_coverage" $IMAGE_BASE_NAME:$CI_API_NAME /bin/bash -c \
    "coverlet ./Menu.API.UnitTests.dll \
    --target dotnet \
    --targetargs \"vstest ./Menu.API.UnitTests.dll\" \
    --format opencover \
    --output /app/coverage.xml \
    && reportgenerator \"-reports:/app/coverage.xml\" \"-targetdir:/app/coveragereport\" " 

    docker cp $(docker ps -aqf "name=$CI_API_NAME_coverage"):/app/coveragereport ./
    docker rm $(docker ps -aqf "name=$CI_API_NAME_coverage")
}

main "$@"
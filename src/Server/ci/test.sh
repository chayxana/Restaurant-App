#!/bin/sh

CI_API_NAME=$1

main () {
    docker_pull

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
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME ./controllers.test -test.coverprofile=coverage.out
}

test_order_api(){
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "java" "-jar" "./order.api-tests.jar"
}

test_identity_api() {
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Identity.API.UnitTests.dll"
}

test_menu_api() {
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Menu.API.UnitTests.dll"
    
    # code coverage 
    docker run -e GITHUB_USER_NAME="$GITHUB_USER_NAME" \
               -e GITHUB_USER_PASSWORD="$GITHUB_USER_PASSWORD" \
               --name "$CI_API_NAME_coverage" \
               $IMAGE_BASE_NAME:$CI_API_NAME /bin/bash -c ./code_coverage.sh | tee output.txt
   
    docker rm $(docker ps -aqf "name=$CI_API_NAME_coverage")
    
    COVERAGE_RESULT=$(grep "Total Branch" output.txt | tr -dc '[0-9]+\.[0-9]')
    
    # if [ $COVERAGE_RESULT -lt 30] && [ $COVERAGE_RESULT -gt 0]; then
    #     BADGE_COLOR="red"
    # elif [ $COVERAGE_RESULT -gt 30 ] && [ $COVERAGE_RESULT -lt 60]; then
    #     BADGE_COLOR="orange"
    # elif [ $COVERAGE_RESULT -gt 60 ] && [ $COVERAGE_RESULT -lt 100]; then
    #     BADGE_COLOR="green"
    # else
    #     exit 1
    # fi
    
    BADGE_COLOR="orange"
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"

    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "coverage" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
}

docker_pull() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
}

main "$@"
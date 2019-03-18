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
    cd ./services/order.api/
    sh test.sh
    cd ../../
    ./ci/sync_folder_s3.sh "$(pwd)/services/order.api/coverage.html" basket_api
    
    # docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME ./controllers.test -test.coverprofile=coverage.out
}

test_order_api(){
    cd ./services/order.api/
    sh test.sh
    cd ../../
    ./ci/sync_folder_s3.sh "$(pwd)/services/order.api/build/reports/jacoco/test/html/" order_api
    
    # docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "java" "-jar" "./order.api-tests.jar"
}

test_identity_api() {
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Identity.API.UnitTests.dll"
}

test_menu_api() {
    docker run --rm $IMAGE_BASE_NAME:$CI_API_NAME "dotnet" "vstest" "./Menu.API.UnitTests.dll"
    
    mkdir menu_api_coverage_report
    
    # code coverage
    docker run -v "$(pwd)"/menu_api_coverage_report:/app/coveragereport \
    --name "$CI_API_NAME_coverage" \
    $IMAGE_BASE_NAME:$CI_API_NAME /bin/bash -c ./code_coverage.sh | tee output.txt
    
    COVERAGE_RESULT=$(grep "Total Branch" output.txt | tr -dc '[0-9]+\.[0-9]')
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"
    
    echo $COVERAGE_RESULT
    echo $BADGE_COLOR
    
    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "coverage" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/menu_api_coverage_report" menu_api
    
    rm -rf menu_api_coverage_report
    docker rm $(docker ps -aqf "name=$CI_API_NAME_coverage")
}

docker_pull() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
}

get_coverage_result_badge_color() {
    RESULT=${$1%.*}
    if [ $RESULT -lt 30 ] && [ $RESULT -gt 0 ]
    then
        BADGE_COLOR="red"
    elif [ $RESULT -gt 30 ] && [ $RESULT -lt 60 ]
    then
        BADGE_COLOR="orange"
    elif [ $RESULT -gt 60 ] && [ $RESULT -lt 100 ]
    then
        BADGE_COLOR="green"
    else
        exit 1
    fi
    echo $BADGE_COLOR
}

main "$@"
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
    cd ./services/basket.api/
    sh test.sh
    cd -
    
    COVERAGE_RESULT=$(sh ./services/basket.api/coverage_result.sh ./services/basket.api/coverage.out)
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"

    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "basket--api" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/services/basket.api/reports/" $CI_API_NAME
}

test_order_api(){
    cd ./services/order.api/
    sh test.sh
    cd -
    
    COVERAGE_RESULT=$(awk -F"," '{ instructions += $4 + $5; covered += $5 } END { rounded = sprintf("%.0f", 100*covered/instructions); print rounded}' ./services/order.api/build/reports/jacoco/test/jacocoTestReport.csv)
    
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"
    
    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "order--api" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/services/order.api/build/reports/jacoco/test/html/" $CI_API_NAME
}

test_identity_api() {
    cd ./services/identity.api/
    sh test.sh | tee output.txt
    cd -

    COVERAGE_RESULT=$(grep "Total Branch" ./services/identity.api/output.txt | tr -dc '[0-9]+\.[0-9]')
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"

    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "identity--api" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/services/identity.api/coveragereport/" $CI_API_NAME
}

test_menu_api() {
    
    cd ./services/menu.api/
    sh test.sh | tee output.txt
    cd -
    
    COVERAGE_RESULT=$(grep "Total Branch" ./services/menu.api/output.txt | tr -dc '[0-9]+\.[0-9]')
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"
    
    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "menu--api" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/services/menu.api/coveragereport/" $CI_API_NAME
    
    # # code coverage
    # docker run -v "$(pwd)"/menu_api_coverage_report:/app/coveragereport \
    #     --name "$CI_API_NAME_coverage" \
    #     $IMAGE_BASE_NAME:$CI_API_NAME /bin/bash -c ./code_coverage.sh | tee output.txt
    
}

docker_pull() {
    docker pull "$IMAGE_BASE_NAME:$CI_API_NAME"
}

get_coverage_result_badge_color() {
    float=$1
    RESULT=${float%.*}
    
    if [ $RESULT -lt 30 ] && [ $RESULT -gt 0 ]
    then
        BADGE_COLOR="red"
    elif [ $RESULT -gt 30 ] && [ $RESULT -lt 60 ]
    then
        BADGE_COLOR="orange"
    elif [ $RESULT -gt 60 ] && [ $RESULT -lt 101 ]
    then
        BADGE_COLOR="green"
    else
        exit 1
    fi
    echo $BADGE_COLOR
}

main "$@"
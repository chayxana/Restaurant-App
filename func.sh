#!/bin/bash

COVERAGE_RESULT=$(grep "Total Branch" output.txt | tr -dc '[0-9]+\.[0-9]')

get_coverage_result_badge_color() {
    COVERAGE_RESULT=$1
    RESULT=${COVERAGE_RESULT%.*}
    
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

NEW_RESULT=$(get_coverage_result_badge_color $COVERAGE_RESULT)

echo $NEW_RESULT
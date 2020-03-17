#!/bin/sh

STATUS=$1
CI_API_NAME=$2
API_TITLE=""
echo $CI_API_NAME

case "$CI_API_NAME" in
        basket_api) API_TITLE="basket--api" ;;
        order_api) API_TITLE="order--api";;
        identity_api) API_TITLE="identity--api" ;;
        menu_api) API_TITLE="menu--api" ;;
        *) echo "Invalid api name!" ;;
    esac

# Set values for shields.io fields based on STATUS
if [ $STATUS = "running" ]; then
	BADGE_SUBJECT="$API_TITLE"
	BADGE_STATUS="running"
	BADGE_COLOR="yellow"
elif [ $STATUS = "failure" ]; then
	BADGE_SUBJECT="$API_TITLE"
	BADGE_STATUS="failed"
	BADGE_COLOR="red"
elif [ $STATUS = "success" ]; then
	BADGE_SUBJECT="$API_TITLE"
	BADGE_STATUS="passed"
	BADGE_COLOR="brightgreen"
else
	exit 1
fi

FILE_NAME="${CI_API_NAME}_build_status.svg"

./ci/generate_badge.sh $FILE_NAME "$BADGE_SUBJECT" "$BADGE_STATUS" $BADGE_COLOR
./ci/upload_badge_s3.sh $FILE_NAME
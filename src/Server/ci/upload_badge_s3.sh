#!/bin/sh

FILE_NAME=$1

aws configure set aws_access_key_id $AWS_KEY
aws configure set aws_secret_access_key $AWS_SECRET
aws configure set default.region eu-central-1

aws s3 cp $FILE_NAME s3://jurabek-restaurant-app/badges/ \
    --grants read=uri=http://acs.amazonaws.com/groups/global/AllUsers \
    --cache-control no-store
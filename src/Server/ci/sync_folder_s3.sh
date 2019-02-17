#!/bin/sh

SOURCE=$1
TARGET=$2

aws configure set aws_access_key_id $AWS_KEY
aws configure set aws_secret_access_key $AWS_SECRET
aws configure set default.region eu-central-1

aws s3 sync $SOURCE s3://jurabek-restaurant-app/coverage/$TARGET --grants read=uri=http://acs.amazonaws.com/groups/global/AllUsers
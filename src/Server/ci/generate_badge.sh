#!/bin/sh

FILE_NAME=$1
SUBJECT=$2
STATUS=$3
COLOR=$4

curl -o $FILE_NAME https://img.shields.io/badge/$SUBJECT-$STATUS-$COLOR.svg
#!/bin/sh

FILE_NAME=$1

curl -s -X POST https://content.dropboxapi.com/2/files/upload \
  --header "Authorization: Bearer $DROPBOX_TOKEN" \
  --header "Content-Type: application/octet-stream" \
  --header "Dropbox-API-Arg: {\"path\":\"$DROPBOX_FOLDER/$FILE_NAME\",\"autorename\":false,\"mute\":false,\"mode\":{\".tag\":\"overwrite\"}}" \
  --data-binary @"$FILE_NAME"


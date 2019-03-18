#!/bin/sh

apk add --no-cache git
CGO_ENABLED=0 go build -installsuffix 'static'
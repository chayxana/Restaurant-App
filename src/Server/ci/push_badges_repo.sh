#!/bin/sh

cd badges
git add -A
DATE=`date +%Y-%m-%d`
COMMIT_MESSAGE="Updating badges-$DATE"
git commit -m"$COMMIT_MESSAGE"
git fetch
git pull
git push -f https://$GITHUB_USER_NAME:$GITHUB_USER_PASSWORD@github.com/Jurabek/restaurant-badges.git master
cd ..
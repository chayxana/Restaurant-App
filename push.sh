BRANCH=develop
for number in {1..10}; do
    git push origin $BRANCH --force-with-lease
    EXIT_CODE=$?
    if [ $EXIT_CODE -eq 0 ]; then
        echo "Git changes pushed into origin/$BRANCH!"
        break 
    else
        echo "Git push failed!"
        sleep 2
        git pull --rebase
        echo "Try to push again!"
    fi
done

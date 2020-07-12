#!/bin/bash
echo "select the operation ************: Type 1 or 2"
echo "1: Minikube"
echo "2: Docker Desktop"

read n
case $n in
  1) echo "You chose Option 1";;
  2) echo "You chose Option 2";;
  *) echo "invalid option";;
esac
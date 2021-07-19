#!/bin/bash
./exportwox.sh
VERSION=`cat VERSION`
PROJECT_NAME="${PWD##*/}" #working folder name
git status
read
git add .
git commit -m "DateTime Formatter (Wox Plugin) - $VERSION"
git push -u origin master
echo Press Enter to Close this Window...
read

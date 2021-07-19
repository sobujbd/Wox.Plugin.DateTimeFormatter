#!/bin/bash

VERSION=`cat VERSION`
PLUGIN_NAME="${PWD##*/}" #working folder name

ExistingWoxFile = $(ls *.wox 2> /dev/null | wc -l)
if [ "$ExistingWoxFile" != "0" ];
then 
    rm *.wox
fi

"7z.exe" a -tzip "$PLUGIN_NAME-$VERSION.wox" ".\Wox.Plugin.DateTimeFormatter\bin\Release\*" -xr!*.pdb -xr!*.xml -xr!*.config

read
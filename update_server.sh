#!/bin/bash
git fetch origin
git reset --hard origin/master
dotnet ef database update
dotnet publish
sudo service supervisor restart
#!/bin/bash

dotnet clean
dotnet build -c Release
dotnet publish -c Release -o publish
cd publish
zip -r ../lambda.zip . 
echo "Completed building zip"
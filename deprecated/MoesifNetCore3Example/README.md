# Moesif .Net Core 3 Example

Important: `appsettings.json` is required to be updated. Please ensure `MoesifOptions` `ApplicationId` is updated in this file, without which this app will fail.

This folder contains sample for `.net Core 3.1`.
Please view the README for `MoesifNet5Example` as it generally applies to .net core 3 also

This sample is identical to the sample `.net 5` which is also part of this repository. The only differences are:
* The naming of `MoesifNetCore3Example` and `MoesifNet5Example`
* in the `.csproj` the use of `<TargetFramework>netcoreapp3.1</TargetFramework>` vs `<TargetFramework>net5.0</TargetFramework>`
* The built binary has path of `bin/Debug/netcoreapp3.1/MoesifNetCore3Example`
* The `.devcontainer` utilizes respective version Dockerfile.

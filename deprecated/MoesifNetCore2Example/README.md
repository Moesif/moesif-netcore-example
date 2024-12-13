# Moesif .Net Core 2 Example
This folder contains sample for `.net Core 2.1`.
Please view the README for `MoesifNet5Example`

This app has been tested on `--runtime linux-x64` target for `netcoreapp 2.1` using included `.devcontainer/devcontainer.json` which runs `ubuntu 20.04`

Important: `appsettings.json` is required to be updated. Please ensure `MoesifOptions` `ApplicationId` is updated in this file, without which this app will fail.

### To Build this app for `linux-x64` runtime

```sh
dotnet clean
dotnet publish --runtime linux-x64 
```
Note: It is important to specify the `--runtime` parameter without which the build will generate a `.dll` but not an executable file.

### To run it:
```sh
bin/Debug/netcoreapp2.1/linux-x64/MoesifNetCore2Example
```
The browser should launch. Just browsing to the page such as `http://localhost:80/` (or some other port if using VSCode) would create an event sent to Moesif. You can also browse to a URL with actual data eg: `http://localhost:80/api/employee/42`

Other available runtimes are published [ here on Microsoft.com site](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) but have not been tested.

This sample is similar to the sample `.net 5` which is also part of this repository.
* The naming of `MoesifNetCore2Example` and `MoesifNet5Example`
* The core parts of how the app is launched
* in the `.csproj` the use of `<TargetFramework>netcoreapp2.1</TargetFramework>` vs `<TargetFramework>net5.0</TargetFramework>`
* The built binary has different output path
* The `.devcontainer` utilizes respective version Dockerfile.

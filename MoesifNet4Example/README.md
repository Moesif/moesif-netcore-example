# Moesif .Net 5 Example

Important: `appsettings.json` is required to be updated. Please ensure `MoesifOptions` `ApplicationId` is updated in this file, without which this app will fail.

### To build this project using dotnet 5

Navigate to `MoesifNet5Example` folder in this repo

```sh
dotnet clean
dotnet build
```

### To run the sample after building it

Navigate to `MoesifNet5Example` folder in this repo
* Ensure `appsettings.json` contains valid `MoesifOptions` > `ApplicationId`
* Run the following command:

```sh
bin/Debug/net5.0/MoesifNet5Example
```

Sample output"
```sh
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /workspaces/moesif-netcore-example/MoesifNet5Example
```
Navigate your web browser to: `http://localhost:5000`
You should see the message:
```sh
Hello World!
```
Now browse to your [Moesif](https://www.moesif.com) portal and the events should appear there shortly.

### Troubleshooting:
* If you see errors such as `Synchronous operations are disallowed. Call Read/Write/FlushAsync or set AllowSynchrnousIO to true instead.`
Look at [Github aspnetcore announcement](https://github.com/dotnet/aspnetcore/issues/7644) for possible workarounds such as 
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true;});
}
```

## To modify this sample in VS Code

This project can be opened in Free [Microsoft Visual Studio Code](https://code.visualstudio.com/). VS Code can run on Windows, MacOs, Debian, Ubuntu, Red Hat, Fedora, SUSE. 

* Set up [VS Code Developing inside a Container](https://code.visualstudio.com/docs/remote/containers)

* Open this folder within VS Code.

* Because this folder contains `.devcontainer` VS Code will prompt if you would like to re-open this project within a container. Select `Yes`.

This sample has been tested using Official Microsoft Docker container `mcr.microsoft.com/vscode/devcontainers/dotnetcore` version 5 - which utilizes `Ubuntu 20.04` as a base image.
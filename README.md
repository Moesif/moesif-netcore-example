# Moesif .NET Core Examples

[Moesif](https://www.moesif.com) is an API analytics and monitoring platform. [Moesif.Middleware](https://github.com/Moesif/moesif-dotnet) is a middleware that makes integration with Moesif easy for .NET applications.

The purpose of this project is to provide examples application with Moesif integrated.
Each of these Examples are independent of each other

|  |  |
|--|--|
| MoesifNet6Example | .Net 6.0 | 
| MoesifNet5Example | .Net 5.0 | 
| MoesifNetCore3Example | .Net Core 3.0 |
| MoesifNetCore2Example | .Net Core 2.0 |

For each example, the general steps to follow are:
1. Update `appsettings.json` with Moesif Application Id
2. Build the project using [Microsoft `dotnet` command utility](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet)
3. Run the newly built code giving it `appsettings.json` input. It launches a web app using the built in `IISExpress` web server.  
4. Browse to the homepage of that newly built web app: `http://localhost:5000`
5. View the events show up in `Moesif` portal.

# Get your Moesif Application Id
Your Moesif Application Id can be found in the [_Moesif Portal_](https://www.moesif.com/).
After signing up for a Moesif account, your Moesif Application Id will be displayed during the onboarding steps. 

You can always find your Moesif Application Id at any time by logging 
into the [_Moesif Portal_](https://www.moesif.com/), click on the top right menu,
and then clicking _Installation_.

## Key files

Moesif Middleware's [github readme](https://github.com/Moesif/moesif-dotnet) already documented
the steps to setup Moesif Middleware. But here are the key file where the Moesif integration is added:

- `Startup.cs` added the Moesif middleware to the pipeline.
- `Settings/MoesifOptions.cs` added Moesif middleware related settings.
- `appsettings.json` is required to be updated. Please ensure `MoesifOptions` `ApplicationId` is updated in this file, without which this app will fail.

## How to build and run this example.

Each sample folder includes its own README.md.

### Samples:
See `EmployeeController.cs` for some sample URLs that you can test such as the below GET:
Replace the port 5000 with the actual port your app is running on

```
GET http://localhost:5000/api/employee/42
```

You can also try a POST request:

```
POST http://localhost:5000/api/employee/
{
    "id": 123,
    "firstName": "first",
    "lastName": "last",
    "dateOfBirth": "0001-01-01T00:00:00"
}
```
The sample API calls should be logged to Moesif. 

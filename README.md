# Moesif .NET Core Example

[Moesif](https://www.moesif.com) is an API analytics and monitoring platform. [Moesif.Middleware](https://github.com/Moesif/moesif-dotnet) is a middleware that makes integration with Moesif easy for .NET applications.

This is an example of .NET Core application with Moesif integrated.

## Key files

Moesif Middleware's [github readme](https://github.com/Moesif/moesif-dotnet) already documented
the steps to setup Moesif Middleware. But here is the key file where the Moesif integration is added:

- `Startup.cs` added the Moesif middleware to the pipeline.
- `Settings/MoesifOptions.cs` added Moesif middleware related settings.

## How to run this example.

1. Install Moesif Middleware if you haven't done so. `Install-Package Moesif.Middleware`

2. Be sure to edit the `Settings/MoesifOptions.cs` to add your Moesif Application Id.

Your Moesif Application Id can be found in the [_Moesif Portal_](https://www.moesif.com/).
After signing up for a Moesif account, your Moesif Application Id will be displayed during the onboarding steps. 

You can always find your Moesif Application Id at any time by logging 
into the [_Moesif Portal_](https://www.moesif.com/), click on the top right menu,
and then clicking _Installation_.

  ```csharp
  Dictionary<string, object> moesifOptions = new Dictionary<string, object>
        {
            {"ApplicationId", 'Your Application ID Found in Settings on Moesif'},
            {"LogBody", true},
            ...
        }
  ```

3. See `EmployeeController.cs` for some sample URLs that you can test such as the below GET:

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

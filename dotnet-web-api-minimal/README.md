# ASP.NET Core Web API Minimal

This sample explores what I consider a _minimal_ installation of ASP.NET Core Web API. It should consist of this command (run from [the sample folder](../dotnet-web-api-minimal)):

```ps1
dotnet new webapi -o Songhay.WebApiOne
```

Then state that static-file support from wwwroot is in play (in [Startup.Configure()](./Songhay.WebApiOne/Startup.cs)):

```c#
app
    .UseDefaultFiles()
    .UseStaticFiles()
    .UseMvc()
    ;
```

Now it is very important to run this from folder of the Web API. So we build/run from the `Songhay.WebApiOne` [folder](./Songhay.WebApiOne):

```ps1
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run
```

The `appsettings.Development.json` [file](./Songhay.WebApiOne/appsettings.Development.json) has been changed from the default to write to the console in the `Development` Environment.

Hitting the conventional `http://localhost:5000` should produce a session like this:

```ps1
Hosting environment: Development
Content root path: .\dotnet-web-api-minimal\Songhay.WebApiOne
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/
info: Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware[2]
      Sending file. Request path: '/index.html'. Physical path: '.\dotnet-web-api-minimal\Songhay.WebApiOne\wwwroot
\index.html'
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 25.8757ms 200 text/html
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/api/values
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
      Executing action method Songhay.WebApiOne.Controllers.ValuesController.Get (Songhay.WebApiOne) with arguments ((null)) - ModelState is Valid
info: Microsoft.AspNetCore.Mvc.Internal.ObjectResultExecutor[1]
      Executing ObjectResult, writing value Microsoft.AspNetCore.Mvc.ControllerContext.
info: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[2]
      Executed action Songhay.WebApiOne.Controllers.ValuesController.Get (Songhay.WebApiOne) in 43.4475ms
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 146.4206ms 200 application/json; charset=utf-8
```

## related links

* “[Setting the environment](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments#setting-the-environment)”
* “[Ordering importance](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/hosting?tabs=aspnetcore2x#ordering-importance)”
* “[Serving a default document](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files#serving-a-default-document)”
* “[LogLevel Enum](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel?view=aspnetcore-2.0)”
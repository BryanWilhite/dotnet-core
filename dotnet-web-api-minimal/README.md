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
dotnet build
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

## adding Razor pages and Razor views

This minimal sample shows that Razor pages _and_ views can be used side by side. One way to quickly see the difference between Razor pages and views is to [see the difference between MVVM and MVC](https://stackify.com/asp-net-razor-pages-vs-mvc/) respectively.

When an ASP.NET Core project starts off with API-only controllers, throwing in Razor pages should come naturally.

## adding Razor pages

The [official documentation on Razor pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start?view=aspnetcore-2.0) does not cover the case where you start with the intent of building an API but you change your mind and you want to add _pages_.

What follows are just a series of conventions:

* add a `Pages` [folder](./Songhay.WebApiOne/Pages) next to the `Controllers` folder
* add a `*.cshtml` [page](./Songhay.WebApiOne/Pages/GroupOne/PageOne.cshtml), making sure to start it with the `@page` directive; the `@page` directive tells Razor that the `.cshtml` file represents a Razor Page
* add the [`_ViewStart.cshtml`](./Songhay.WebApiOne/Pages/_ViewStart.cshtml) page and its referenced [`_Layout.cshtml`](./Songhay.WebApiOne/Pages/_Layout.cshtml) page

It should help to mention the `_ViewImports.cshtml` [page](https://www.learnrazorpages.com/razor-pages/files/viewimports) which would definitely be a helpful addition to these minimal conventions.

## adding Razor views

The minimal path to Razor views recommended here starts with adding a new controller such as [`MyViewController.cs`](./Songhay.WebApiOne/Controllers/MyViewController.vs). This is done for the sake of organization, not for any technical reason as we can see that _all_ controllers derive from `Controller`. Also, note that the `IApplicationBuilder.UseMvc()` extension method in [`Startup.cs`](./Songhay.WebApiOne/Startup.cs#L40) is enough to support Razor views.

What follows are just a series of conventions:

* add a `Views` [folder](./Songhay.WebApiOne/Views) next to the `Controllers` folder
* add a `*.cshtml` [file](./Songhay.WebApiOne/Views/MyView/ViewOne.cshtml)
* add the [`_ViewStart.cshtml`](./Songhay.WebApiOne/Views/_ViewStart.cshtml) page and its referenced [`_Layout.cshtml`](./Songhay.WebApiOne/Views/Shared/_Layout.cshtml) page

## related links

* [ASP.NET Core Static Files](../dotnet-static-content)
* “[Setting the environment](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments#setting-the-environment)”
* “[Ordering importance](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/hosting?tabs=aspnetcore2x#ordering-importance)”
* “[Serving a default document](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files#serving-a-default-document)”
* “[LogLevel Enum](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.LogLevel?view=aspnetcore-2.0)”
* “[ASP.NET Core Razor Pages—Introduction](https://codingblast.com/asp-net-core-razor-pages/)”
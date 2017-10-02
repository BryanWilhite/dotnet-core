# ASP.NET Core and Node Services

This sample build upon the work in the `dotnet-node-services` [folder](../dotnet-node-services). The `Microsoft.AspNetCore.SpaServices` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.SpaServices/)] depends on `Microsoft.AspNetCore.NodeServices`. Choosing the `SpaServices` package allows us to use `asp-prerender-module` Tag Helper in an MVC project with a `_ViewImports.cshtml` file.

```ps1
dotnet new mvc
dotnet add package Microsoft.AspNetCore.SpaServices --version 2.0.0
npm i -S aspnet-prerendering
```

## related links

* “[Using JavaScriptServices for Creating Single Page Applications with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa-services)”
* “[Building Single Page Applications on ASP.NET Core with JavaScriptServices](https://blogs.msdn.microsoft.com/webdev/2017/02/14/building-single-page-applications-on-asp-net-core-with-javascriptservices/)”
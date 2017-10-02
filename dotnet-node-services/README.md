# ASP.NET Core and Node Services

This sample highlights the pre-rendering capabilities of ASP.NET core with `Microsoft.AspNetCore.NodeServices` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.NodeServices/)] and `aspnet-prerendering` [[npm](https://www.npmjs.com/package/aspnet-prerendering)]. This comes from [the presentation](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Angular-and-NET-Core#time=13m45s) by [Ed Charbeneau](https://twitter.com/EdCharbeneau), a guest of Microsoft.

```ps1
dotnet new web
dotnet add package Microsoft.AspNetCore.NodeServices --version 2.0.0
npm i -S aspnet-prerendering
npm install --save-dev @types/node
```

To see Node Services exposed to the MVC Tag Helper components, go to the `dotnet-spa-services` [sample](../dotnet-spa-services).

## related links

* [Visual Studio Toolbox: “Angular and .NET Core”](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Angular-and-NET-Core#time=13m45s)
* “[Using JavaScriptServices for Creating Single Page Applications with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa-services)”

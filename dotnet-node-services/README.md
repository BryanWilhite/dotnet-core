# ASP.NET Core and Node Services

This sample highlights the pre-rendering capabilities of ASP.NET core with `Microsoft.AspNetCore.NodeServices` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.NodeServices/)] and `aspnet-prerendering` [[npm](https://www.npmjs.com/package/aspnet-prerendering)]. This comes from [the presentation](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Angular-and-NET-Core#time=13m45s) by [Ed Charbeneau](https://twitter.com/EdCharbeneau), a guest of Microsoft.

To get this sample working, we first run this:

```bash
dotnet new web -o Songhay.NodeOne
```

The `dotnet new web` will generate a project file that will reference `Microsoft.AspNetCore.All` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.all)] which includes `Microsoft.AspNetCore.NodeServices`.

Now it is very important to run this from folder of the Web API. So we build/run from the `Songhay.NodeOne` [folder](./Songhay.NodeOne):

```bash
npm i --save-dev aspnet-prerendering
npm i --save-dev @types/node

dotnet build
dotnet run
```

Hitting the conventional `http://localhost:5000` should produce a session like this:

```bash
Hosting environment: Development
Content root path: .\dotnet-node-services\Songhay.NodeOne
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 1877.1451ms 200
```

## related links

* [Visual Studio Toolbox: “Angular and .NET Core”](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Angular-and-NET-Core#time=13m45s)
* “[Using JavaScriptServices for Creating Single Page Applications with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa-services)”

@[BryanWilhite](https://twitter.com/BryanWilhite)

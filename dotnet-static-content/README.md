# ASP.NET Core Static Files

This sample isolates and demonstrates _exactly_ what is needed to get ASP.NET Static Files [[GitHub](https://github.com/aspnet/StaticFiles)] working in ASP.NET Core. This sample is completely ignorant of these advanced static-file topics:

* setting MIME types with `FileExtensionContentTypeProvider`<sup>*</sup> [[docs](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider?view=aspnetcore-2.0)]
* setting a default document
* enabling directory browsing
* enabling `Cache-Control`
* enabling static file authorization via an MVC controller
* using `IServiceCollection.AddDirectoryBrowser()`

These topics (and more) are introduced in “[Working with static files in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files).”

<sup>*</sup><small>note that, “the ASP.NET static file middleware understands almost 400 known file content types.”</small>

To get this minimal sample to work we first run this:

```ps1
dotnet new web -o Songhay.StaticOne
```

Then add something like `static_file.html` [[view](./Songhay.StaticOne/wwwroot/static_file.html)] and add these lines to `Startup.Configure()` [[view](./Songhay.StaticOne/Startup.cs)]:

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync(@"<a href=""./static_file.html"">Hello World!</a>");
    });
}
```

The most important statement above is `app.UseStaticFiles()`.

Now it is very important to run this from folder of the Web API. So we build/run from the `Songhay.StaticOne` [folder](./Songhay.StaticOne):

```ps1
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet build
dotnet run
```

Hitting the conventional `http://localhost:5000` should produce a session like this:

```ps1
Hosting environment: Development
Content root path: .\dotnet-static-content\Songhay.StaticOne
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 15.204ms 200
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:5000/static_file.html
info: Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware[2]
      Sending file. Request path: '/static_file.html'. Physical path: '.\dotnet-static-content\Songhay.StaticOne\ww
wroot\static_file.html'
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 27.3668ms 200 text/html
```

## `AspNetCore.StaticSiteHelper`

[Mads Kristensen](https://twitter.com/mkristensen) released `AspNetCore.StaticSiteHelper` for .NET Core 1.1. His [dotnet template](http://dotnetnew.azurewebsites.net/template/MadsKristensen.AspNetCore.Web.Templates/madsk.static.web) for this is awesome. However, we will need a .NET Core 2.0 version.
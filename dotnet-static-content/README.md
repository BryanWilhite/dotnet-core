# ASP.NET Core Static Files

This sample isolates and demonstrates _exactly_ what is needed to get ASP.NET Static Files [[GitHub](https://github.com/aspnet/StaticFiles)] working in ASP.NET Core. This sample is completely ignorant of these advanced static-file topics:

* setting MIME types with `FileExtensionContentTypeProvider`<sup>*</sup> [[docs](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider?view=aspnetcore-2.0)]
* setting a default document
* enabling directory browsing
* enabling `Cache-Control`
* enabling static file authorization via an MVC controller
* using `IServiceCollection.AddDirectoryBrowser()`

These topics (and more) are introduced in “[Working with static files in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files).”

To get this minimal sample to work we first run this:

```ps1
dotnet new web
```

<sup>*</sup> note that, “the ASP.NET static file middleware understands almost 400 known file content types.”

Then add something like `static_file.html` [[view](./wwwroot/static_file.html)] and add this line to `Startup.cs` [[view](./Startup.cs)]:

```c#
app.UseStaticFiles();
```

## `AspNetCore.StaticSiteHelper`

[Mads Kristensen](https://twitter.com/mkristensen) released `AspNetCore.StaticSiteHelper` for .NET Core 1.1. His [dotnet template](http://dotnetnew.azurewebsites.net/template/MadsKristensen.AspNetCore.Web.Templates/madsk.static.web) for this is awesome. However, we will need a .NET Core 2.0 version.
# ASP.NET Core Static Files

This sample isolates and demonstrates _exactly_ what is needed to get ASP.NET Static Files [[📖 docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-8.0)] working in ASP.NET Core.

To get this minimal sample to work we first run this (from the `dotnet-web-static-content` [directory](../dotnet-web-static-content)):

```bash
dotnet new web -o Songhay.StaticOne/Songhay.StaticOne

dotnet new sln -n Songhay.StaticOne -o Songhay.StaticOne

dotnet sln Songhay.StaticOne/Songhay.StaticOne.sln \
      add Songhay.StaticOne/Songhay.StaticOne/Songhay.StaticOne.csproj

mkdir Songhay.StaticOne/Songhay.StaticOne/wwwroot
touch Songhay.StaticOne/Songhay.StaticOne/wwwroot/static_file.html
```

In `static_file.html` we have:

```html
<!DOCTYPE html>
<html>
    <head>
        <meta name="description" content="This is a sample for my self-education." />
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />

        <title>Static File</title>
    </head>
    <body>
        <h1>Static File</h1>
        <p>Hello world!</p>
    </body>
</html>
```

and add these lines to the `Program.cs` [file](../dotnet-web-static-content/Songhay.StaticOne/Songhay.StaticOne/Program.cs):

```csharp
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/", async context =>
    {
        string inlineHtml = @"<a href=""./static_file.html"">Hello World!</a>";
        await context.Response.WriteAsync(inlineHtml);
    });

app.UseStaticFiles();

app.Run();
```

The most important statement above is `app.UseStaticFiles()`. The `MapGet` expression is just a convenient way to generate HTML without having to define another static file.

Finally:

```bash
dotnet run --project Songhay.StaticOne/Songhay.StaticOne/Songhay.StaticOne.csproj
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

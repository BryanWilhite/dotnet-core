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

Hitting the conventional `http://localhost:5000` should produce a session like this:

```ps1

```
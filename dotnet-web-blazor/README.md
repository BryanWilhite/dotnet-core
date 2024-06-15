# Blazor

> Blazor is a framework for building interactive client-side web UI with [.NET](https://learn.microsoft.com/en-us/dotnet/standard/tour):
>
> - Create rich interactive UIs using [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) instead of [JavaScript](https://www.javascript.com).
> - Share server-side and client-side app logic written in .NET.
> - Render the UI as HTML and CSS for wide browser support, including mobile browsers.
> - Integrate with modern hosting platforms, such as [Docker](https://learn.microsoft.com/en-us/dotnet/standard/microservices-architecture/container-docker-introduction/index).
> - Build hybrid desktop and mobile apps with .NET and Blazor.
>
>—[Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0)
>

In this .NET 8.0 time-frame, there are three Blazor project types:

```console
Blazor Server App                     blazorserver                [C#]        Web/Blazor
Blazor Web App                        blazor                      [C#]        Web/Blazor/WebAssembly
Blazor WebAssembly Standalone App     blazorwasm                  [C#]        Web/Blazor/WebAssembly/PWA
```

We are just going to look at the default project layouts:

## `blazor` app

I assume this is the new hybrid-render-mode Blazor app:

```bash
dotnet new blazor -o MyBlazorHybrid
cd MyBlazorHybrid
dotnet run
```

## `blazorserver` app

This project type is clearly deprecated as it generates a .NET 6.0 project:

```bash
dotnet new blazorserver -o MyBlazorServer
cd MyBlazorServer
dotnet run
```

## `blazorwasm` app

```bash
dotnet new blazorwasm -o MyBlazorWasm
cd MyBlazorWasm
dotnet run
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

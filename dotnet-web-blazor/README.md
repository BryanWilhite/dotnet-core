# Blazor

## `blazorserver` app

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

## in the `MyBlazorWasm.Desktop` directory

The `MyBlazorWasm.Desktop` [directory](./MyBlazorWasm.Desktop) is a copy of the `MyBlazorWasm` [directory](./MyBlazorWasm) and then the following rather unusual manual changes are made:

### add new packages

The following packages need to be added:

- `Microsoft.Extensions.Logging.Debug`
- `Photino.Blazor`

### use `Microsoft.NET.Sdk.Razor` and declare `WinExe` output

Change:

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
```

…to:

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
```

…and add:

```xml
<OutputType>WinExe</OutputType>
```

### remove `Microsoft.AspNetCore.Components.WebAssembly*` packages

The following packages should be removed:

- `Microsoft.AspNetCore.Components.WebAssembly`
- `Microsoft.AspNetCore.Components.WebAssembly.DevServer`

### use `PhotinoBlazorAppBuilder` in `Program.cs`

Rewrite the `Program.cs` file like [this](./MyBlazorWasm.Desktop/Program.cs):

```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorWasm;

using Photino.Blazor;

/*
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
*/

var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

appBuilder.Services.AddLogging();
appBuilder.RootComponents.Add<App>("app");
appBuilder.RootComponents.Add<HeadOutlet>("head::after");
//appBuilder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = appBuilder.Build();
app.MainWindow
    .SetTitle("Photino Blazor Sample");

AppDomain.CurrentDomain.UnhandledException += (_, error) =>
{
    app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
};

app.Run();
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

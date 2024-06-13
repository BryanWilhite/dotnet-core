# the .NET Generic Host

The _.NET Generic Host_ \[📖 [docs](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host) \] has been available since [[dotnet|.NET 6.0]] ([2021](https://en.wikipedia.org/wiki/.NET)):

>A _host_ is an object that encapsulates an app’s resources and lifetime functionality, such as:
>
> - Dependency injection (DI)
> - Logging
> - Configuration
> - App shutdown
> - `IHostedService` implementations \[ for long-running background tasks \[📖 [docs](https://learn.microsoft.com/en-us/dotnet/core/extensions/timer-service?pivots=dotnet-7-0) \] \]
>

Essentially, the organized environment and rich context established for ASP.NET developers has been made available (finally) to _all_ .NET developers. It is a tragic misunderstanding to conclude that the .NET Generic Host is “only” for developing Windows services. Again, this is for _all_ .NET developers—and we no longer have to think about rolling our own (or installing our own) dependency injection, logging or command-line handling (configuration) infrastructure.

The .NET Generic Host starts with one NuGet package: `Microsoft.Extensions.Hosting` [📦 [NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)]

It is important to look at [the dependencies](https://www.nuget.org/packages/Microsoft.Extensions.Hosting) of this package to understand that there should no longer be a frequent need to _individually_ install packages like:

- `Microsoft.Extensions.Configuration*`
- `Microsoft.Extensions.DependencyInjection*`
- `Microsoft.Extensions.Logging*`

Microsoft intends to roll all of these packages up under _one_ conceptual façade.

## setup

From the `/dotnet-generic-host` [directory](../dotnet-generic-host):

```bash
dotnet new console -o My.GenericHost
dotnet add My.GenericHost/My.GenericHost.csproj package Microsoft.Extensions.Hosting
dotnet new sln -n My.GenericHost
dotnet sln My.GenericHost.sln add My.GenericHost/My.GenericHost.csproj
```

Add a new file, `app-settings.json`, under the `/dotnet-generic-host/My.GenericHost` [directory](../dotnet-generic-host/My.GenericHost) with the following content:

```json
{
    "defaultTraceSourceName": "songhay-log",
    "connectionStrings": {
        "contosoOne":
            "Persist Security Info=False;Integrated Security=SSPI; database=AdventureWorks;server=(local)"
    },
    "myDictionary": {
        "one": "uno",
        "two": "dos",
        "three": "tres",
        "four": "quatro"
    }
}
```

Edit the project [file](../dotnet-generic-host/My.GenericHost/My.GenericHost.csproj), `dotnet-generic-host/My.GenericHost/My.GenericHost.csproj`, adding:

```xml
<ItemGroup>
    <Content Include="app-settings.json">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```

Edit the `/dotnet-generic-host/My.GenericHost/Program.cs` [file](../dotnet-generic-host/My.GenericHost/Program.cs) with the following contents:

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using My.Hosting;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.ConfigureHostConfiguration(builder =>
{
    builder.AddCommandLine(["--ENVIRONMENT", Environments.Development]);
});

builder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
{
    configBuilder.AddJsonFile("app-settings.json", optional: false);
});

builder.ConfigureServices((hostingContext, services) => 
{
    services.AddLogging();
    services.AddHostedService<MyHostedService>();
});

IHost host = builder.Build();
host.Run();
```

Add a new `*.cs` file, `/dotnet-generic-host/My.GenericHost/MyHostedService.cs` with the following contents:

```csharp
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace My.Hosting;

public class MyHostedService: IHostedService
{
    public MyHostedService(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime, ILogger<MyHostedService> logger)
    {
        _configuration = configuration;
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(() =>
        {
            _logger.LogInformation("Displaying configuration...");

            IReadOnlyCollection<string> keys = _configuration
                .AsEnumerable().Select(kv => kv.Key).ToArray();

            StringBuilder sb = new();

            foreach (string key in keys)
            {
                sb.AppendFormat("    {0}: {1}{2}", key, _configuration[key], Environment.NewLine);
            }

            _logger?.LogInformation("{Lines}", sb.ToString());

            _logger?.LogWarning("Stopping application...");

            _exitCode = 0;
            _hostApplicationLifetime.StopApplication();
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("Stopping `{Name}`...", nameof(MyHostedService));

        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        // FUNKYKB: exit code may be null should use enter Ctrl+c/SIGTERM.

        return Task.CompletedTask;
    }

    readonly IConfiguration _configuration;
    readonly IHostApplicationLifetime _hostApplicationLifetime;
    readonly ILogger<MyHostedService> _logger;

    int? _exitCode;
}
```

## running this sample

From the `/dotnet-generic-host/My.GenericHost` directory, enter:

```bash
dotnet run
```

## the purpose of `IHostedService`

The ASP.NET MVC developer knows that the _C_ in MVC stands for _controller_. The controller is effectively the entry point of the “business logic” of your application. The entry point for your code over the .NET Generic Host is the class implementing `IHostedService`—this is `MyHostedService` in our example above.

The .NET Generic Host `Program` prepares an environment to ultimately call this one line of code:

```csharp
services.AddHostedService<MyHostedService>();
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

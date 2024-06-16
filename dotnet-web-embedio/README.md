# EmbedIO, the tiny .NET web server

<https://github.com/unosquare/embedio#readme>

From my squalid point of view, EmbedIO is like `http-server` [[npm](https://www.npmjs.com/package/http-server)] from the Node.js ecosystem but you can refer to it in .NET code and do way, _way_ more with it. But let’s stay squalid for a few more ticks and approach EmbedIO as a simple static file server (like `http-server`).

## EmbedIO as a simple static file server

Start by generating the classic `hwapp` console app from [this directory](../dotnet-web-embedio):

```bash
dotnet new console -o hwapp

cd hwapp
dotnet add package embedio
```

Next we rewrite the default `Program.cs` [file](./hwapp/Program.cs) with the guidance of [the documented sample code](https://github.com/unosquare/embedio#webserver-setup) and the understanding that we are using [top-level statement syntax](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements):

```csharp
using System.Diagnostics;
using EmbedIO;
using EmbedIO.Files;
using Swan.Logging;

var url = "http://localhost:9696/";
if (args.Length > 0) url = args[0];

using var server = GetWebServer(url);

server.RunAsync();

var browser = new Process()
{
    StartInfo = new ProcessStartInfo(url) { UseShellExecute = true }
};

browser.Start();

Console.ReadKey(true);

static WebServer GetWebServer(string url)
{
    var server = new WebServer(o => o
        .WithUrlPrefix(url)
        .WithMode(HttpListenerMode.EmbedIO))
        .WithStaticFolder( // Add static files after other modules to avoid conflicts
            "/",
            $"{AppDomain.CurrentDomain.BaseDirectory}wwwroot",
            true,
            m => m.WithContentCaching(false));

    server.StateChanged += (_, e) =>
    {
        if (e.NewState == WebServerState.Loading)
        {
            $"{nameof(AppDomain.CurrentDomain.BaseDirectory)}: {AppDomain.CurrentDomain.BaseDirectory}".Info();
        }

        $"{nameof(server.StateChanged)}: {e.NewState}".Info();
    };

    return server;
}
```

Next, we will need a simple `/dotnet-web-embedio/hwapp/wwwroot` [directory](../dotnet-web-embedio/hwapp/wwwroot) with two files:

```console
./hwapp/wwwroot/
├── favicon.ico
└── index.html
```

Finally, we will want to copy the `/wwwroot` directory to the .NET build root. This means declaring an `ItemGroup` in the `/dotnet-web-embedio/hwapp/hwapp.csproj` [file](../dotnet-web-embedio/hwapp/hwapp.csproj):

```xml
<ItemGroup>
    <None Include=".\wwwroot\**\*">
    <Link>wwwroot\%(RecursiveDir)/%(FileName)%(Extension)</Link>
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

Alternatively, we can declare:

```xml
<ItemGroup>
    <Content Include="wwwroot\**\*.*">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```

We run all of this from the `/dotnet-web-embedio/hwapp` [directory](../dotnet-web-embedio/hwapp):

```bash
dotnet run
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

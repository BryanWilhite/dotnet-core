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

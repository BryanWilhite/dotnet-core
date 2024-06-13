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

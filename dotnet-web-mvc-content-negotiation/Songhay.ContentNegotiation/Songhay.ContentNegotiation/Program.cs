using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Songhay.ContentNegotiation;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host
        .CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((_, configBuilder) => configBuilder.AddEnvironmentVariables())
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

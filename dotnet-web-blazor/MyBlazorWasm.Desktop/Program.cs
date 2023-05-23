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

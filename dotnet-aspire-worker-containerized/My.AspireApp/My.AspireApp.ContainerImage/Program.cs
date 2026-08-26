using My.AspireApp.ContainerImage;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddHealthChecks();

var host = builder.Build();
host.Run();

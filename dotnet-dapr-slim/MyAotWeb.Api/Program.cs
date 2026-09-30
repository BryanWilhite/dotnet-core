using System.Text.Json.Serialization;
using Dapr.Client;
using Microsoft.AspNetCore.Http.Json;

const string StateStoreName = "state-store";    // matches metadata.name
                                                // in sqlite-state-store.yaml
const string StateStoreKey = "myKey";

Uri? appUri = null;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<JsonOptions>(options => options
    .SerializerOptions
    .TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default))
    .AddDaprClient();

WebApplication app = builder.Build();

DaprClient runtimeClient = app.Services.GetRequiredService<DaprClient>();

runtimeClient.JsonSerializerOptions.TypeInfoResolverChain.Clear();
runtimeClient.JsonSerializerOptions
    .TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    appUri = new("http://localhost:5123");
}

app.MapPost("/save", async (DaprClient runtimeClient) =>
{
    await runtimeClient.SaveStateAsync(StateStoreName, StateStoreKey, "Hello from SQLite!");

    return Results.Ok("State saved!");
});

app.MapGet("/get", async (DaprClient runtimeClient) =>
{
    string value = await runtimeClient
        .GetStateAsync<string>(StateStoreName, StateStoreKey);

    return Results.Ok(new Dictionary<string, string> { [StateStoreKey] = value });
});

app.Run(appUri?.OriginalString);

[JsonSerializable(typeof(IEnumerable<DaprClient>))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Dictionary<string, string>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;

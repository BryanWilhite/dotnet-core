using Dapr.Client;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDaprClient();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/save", async (DaprClient daprClient) =>
{
    // "statestore" matches the metadata.name in your component YAML file
    await daprClient.SaveStateAsync("statestore", "myKey", "Hello from SQLite!");

    return Results.Ok("State saved!");
});

app.MapGet("/get", async (DaprClient daprClient) =>
{
    string value = await daprClient.GetStateAsync<string>("statestore", "myKey");

    return Results.Ok(new { Value = value });
});

app.Run();

# .NET and dapr “slim”

The attempt here is to run a dapr app with a single ASP.NET API in “slim” mode. Most of the instructions here are from Google Gemini and the 2026 article “[How to Configure Dapr with SQLite State Store](https://oneuptime.com/blog/post/2026-03-31-dapr-sqlite-state-store/view),” written for JavaScript.

From the `dotnet-dapr-slim` [directory](../dotnet-dapr-slim):

```bash
mkdir .dapr/components
touch .dapr/components/sqlite-statestore.yaml
```

In the `sqlite-statestore.yaml` [file](./.dapr/components/sqlite-statestore.yaml) add:

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: statestore
spec:
  type: state.sqlite
  version: v1
  metadata:
    # Path to the database file (created automatically if it doesn't exist)
    - name: connectionString
      value: "file:./dapr-state.db"
    - name: tableName
      value: "state"
```

Add a ‘slim’ ASP.NET minimal Web API project:

```bash
dotnet new webapiaot -o MyAotWeb.Api
```

Initialize a “slim” dapr host and see output similar to the following:

```shell
$ dapr init --slim

⌛  Making the jump to hyperspace...
ℹ️  Installing runtime version 1.18.4
↓  Downloading binaries and setting up components...
Dapr runtime installed to ~/.dapr/bin, you may run the following to add it to your path if you want to run daprd directly:
    export PATH=$PATH:~/.dapr/bin
✅  Downloading binaries and setting up components...
✅  Downloaded binaries and completed components set up.
ℹ️  daprd binary has been installed to ~/.dapr/bin.
ℹ️  placement binary has been installed to ~/.dapr/bin.
ℹ️  scheduler binary has been installed to ~/.dapr/bin.
✅  Success! Dapr is up and running. To get started, go here: https://docs.dapr.io/getting-started
```

Add the dapr NuGet package to our ASP.NET project:

```bash
dotnet add ./MyAotWeb.Api/MyAotWeb.Api.csproj package Dapr.Client
dotnet add ./MyAotWeb.Api/MyAotWeb.Api.csproj package Dapr.AspNetCore
dotnet new sln -n MyDapr
dotnet sln add ./MyAotWeb.Api/MyAotWeb.Api.csproj
```

Edit the `Program.cs` [file](./MyAotWeb.Api/Program.cs) such that:

- `using Dapr.Client;` is inserted at line 1
- remove any `ConfigureHttpJsonOptions` calls
- remove the `Todos` routes types, and `JsonSerializerContext`
- add `builder.Services.AddDaprClient();`
- add the dapr routes:

```csharp
app.MapPost("/save", async (DaprClient daprClient) =>
{
    // "statestore" matches the metadata.name in your component YAML file
    await daprClient.SaveStateAsync("statestore", "myKey", "Hello from SQLite!");

    return Results.Ok("State saved!");
});

app.MapGet("/get", async (DaprClient daprClient) =>
{
    var value = await daprClient.GetStateAsync<string>("statestore", "myKey");

    return Results.Ok(new { Value = value });
});
```
Run dapr with the following command:

```bash
dapr run --app-id my-aot-web-app --app-port 5123 \
    --resources-path ./.dapr/components \
    --scheduler-host-address "" \
    --placement-host-address "" \
    -- dotnet run --project ./MyAotWeb.Api/MyAotWeb.Api.csproj
```

…where:

- `--app-id` is defined in place
- `--scheduler-host-address` and `--placement-host-address` are explicitly set to empty strings to prevent “slim” mode error messages

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

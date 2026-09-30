# .NET and dapr “slim” initialization

The attempt here is to run a dapr app with a single, <acronym title="ahead of time">AOT</acronym>-compiled ASP.NET minimal API from a “slim” initialization [📖 [docs](https://docs.dapr.io/getting-started/install-dapr-selfhost#slim-init)]. This kind of initialization omits the use of containers during the development process which one may find incredibly convenient or, at the very least, transitional. The promise that comes with “slim” initialization assures us that all we have to do to switch over to containers is to change configuration assets exclusively. No changes in code are required.

Many of the instructions listed below are from Google Gemini and the 2026 article “[How to Configure Dapr with SQLite State Store](https://oneuptime.com/blog/post/2026-03-31-dapr-sqlite-state-store/view),” written for JavaScript.

## configure the dapr state store component

From the `dotnet-dapr-slim` [directory](../dotnet-dapr-slim):

```bash
mkdir .dapr/components
touch .dapr/components/sqlite-state-store.yaml
mkdir db
```

In the `sqlite-statestore.yaml` [file](./.dapr/components/sqlite-statestore.yaml) add:

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: state-store
spec:
  type: state.sqlite
  version: v1
  metadata:
    # Path to the database file (created automatically if it doesn't exist)
    - name: connectionString
      value: "file:./db/dapr-state.db"
    - name: tableName
      value: "state"
```

## add the ASP.NET project

Add a ‘slim’ ASP.NET minimal Web API project:

```bash
dotnet new webapiaot -o MyAotWeb.Api
```

Add the dapr NuGet package to our ASP.NET project:

```bash
dotnet add ./MyAotWeb.Api/MyAotWeb.Api.csproj package Dapr.Client
dotnet add ./MyAotWeb.Api/MyAotWeb.Api.csproj package Dapr.AspNetCore
dotnet new sln -n MyDapr
dotnet sln add ./MyAotWeb.Api/MyAotWeb.Api.csproj
```

Edit the `Program.cs` [file](./MyAotWeb.Api/Program.cs) such that:

- add `using Dapr.Client;`
- add state-store constants to centralize strings used in routes
- add `Uri? appUri` to hard-code the local port for development (this is needed whether or not a proper `/MyAotWeb.Api/Properties/launchSettings.json` is generated)
- remove the `Todos` routes and types
- add the dapr routes 
- add the `builder.Services.AddDaprClient();` call [📖 [docs](https://docs.dapr.io/developing-applications/sdks/dotnet/dotnet-client/#http)]
- change `AppJsonSerializerContext` to support types returned from the dapr routes
- get `DaprClient` from the <acronym title="Inversion of Control">IoC</acronym> container and inject `AppJsonSerializerContext`

See the `Program.cs` [file](./MyAotWeb.Api/Program.cs) to observe how the changes above work together.

## execute “slim” initialization with the dapr CLI

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

## run dapr

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

In the output of the log, we should see messages like:

```console
INFO[0000] Component loaded: state-store (state.sqlite/v1)  app_id=my-aot-web-app

…

INFO[0000] application protocol: http. waiting on port 5123.  This will block until the app is listening on that port.  app_id=my-aot-web-app

…

INFO[0003] application discovered on port 5123           app_id=my-aot-web-app

…

✅  You're up and running! Both Dapr and your app logs will appear here.
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

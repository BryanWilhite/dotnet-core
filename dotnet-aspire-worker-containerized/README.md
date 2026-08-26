# Aspire with .NET worker: containerized

This sample builds upon the work done in the `dotnet-worker-containerized` [directory](../dotnet-worker-containerized).

## setup

From the `dotnet-aspire-worker-containerized` [directory](../dotnet-aspire-worker-containerized):

```bash
dotnet new aspire -o My.AspireApp
```

From the `My.AspireApp` [directory](./My.AspireApp):

```bash
dotnet new worker -o My.AspireApp.ContainerImage -n My.AspireApp.ContainerImage

dotnet sln My.AspireApp.sln add ./My.AspireApp.ContainerImage/My.AspireApp.ContainerImage.csproj

dotnet add \
    ./My.AspireApp.ContainerImage/My.AspireApp.ContainerImage.csproj \
    reference \
    ./My.AspireApp.ServiceDefaults/My.AspireApp.ServiceDefaults.csproj

dotnet add \
    ./My.AspireApp.AppHost/My.AspireApp.AppHost.csproj \
    reference \
    ./My.AspireApp.ContainerImage/My.AspireApp.ContainerImage.csproj
```

## register the service in Aspire Host

In the `AppHost.cs` [file](./My.AspireApp/My.AspireApp.AppHost/AppHost.cs):

```csharp
builder.AddProject<Projects.My_AspireApp_ContainerImage>("my-aspire-worker");
```

...where `"my-aspire-worker"` is an arbitrary name that will be used in the [Aspire Dashboard](https://aspire.dev/dashboard/explore/).

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

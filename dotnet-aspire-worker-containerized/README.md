# Aspire with .NET worker: containerized

This is a faithful walkthrough of “[Containerize a .NET app with dotnet publish](https://learn.microsoft.com/en-us/dotnet/core/containers/sdk-publish).”

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

## register the service in Aspire



## publish

```bash
dotnet publish --os linux --arch x64 /t:PublishContainer
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

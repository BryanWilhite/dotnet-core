# .NET worker: containerized

This is a faithful walkthrough of “[Containerize a .NET app with dotnet publish](https://learn.microsoft.com/en-us/dotnet/core/containers/sdk-publish).”

## setup

From the `dotnet-worker-containerized` [directory](../dotnet-worker-containerized):

```bash
dotnet new worker -o Worker -n DotNet.ContainerImage

dotnet new sln --name DotNet.ContainerImage

dotnet sln DotNet.ContainerImage.slnx add Worker/DotNet.ContainerImage.csproj
```

## publish

```bash
dotnet publish --os linux --arch x64 /t:PublishContainer
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

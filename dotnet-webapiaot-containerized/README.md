# ASP.NET Core Web API (native AOT), containerized (without local HTTPS)

We are just going to run `dotnet new webapiaot` where `webapiaot` is a default project template called `ASP.NET Core Web API (native AOT)` that should show up in `dotnet new list`. With that prerequisite, we are going to:

1. generate the `ASP.NET Core Web API (native AOT)` project and add a Solution file
2. change the `ASP.NET Core Web API (native AOT)` project file for containers
3. change the default `*.http` file to point at the container port
4. publish the `ASP.NET Core Web API (native AOT)` project to the local Podman container host

## generate the `ASP.NET Core Web API (native AOT)` project and add a Solution file

From the `dotnet-webapiaot-containerized` [directory](../dotnet-webapiaot-containerized):

```bash
dotnet new webapiaot -o MyAotWeb.Api

dotnet new sln -n MyAotWeb.Api
dotnet sln MyAotWeb.Api.slnx add ./MyAotWeb.Api/MyAotWeb.Api.csproj
```

## change the `ASP.NET Core Web API (native AOT)` project file for containers

Edit the `MyAotWeb.Api.csproj` [file](./MyAotWeb.Api/MyAotWeb.Api.csproj) to add:

- `<Version>`, a MSBuild property
- `<Title>`, `<Description>`, `<Authors>`, `<Copyright>` and `<Company>` MSBuild properties
- `<ContainerImageTag>`, a MSBuild property bound to `<Version>`
- `<LocalRegistry>`, the MSBuild property set to `Podman` because this is installed on my Linux desktop

…and the following `<ContainerEnvironmentVariable>` declarations:

```xml
<ItemGroup>
    <ContainerEnvironmentVariable Include="ASPNETCORE_ENVIRONMENT" Value="Production" />
    <ContainerEnvironmentVariable Include="DOTNET_ENVIRONMENT" Value="Production" />
    <ContainerEnvironmentVariable Include="HTTP_PORTS" Value="8080" />
</ItemGroup>
```

…where `HTTP_PORTS` is set to `8080`, the port the container will be listening on.

## change the default `*.http` file to point at the container port

Edit the `MyAotWeb.Api.http` [file](./MyAotWeb.Api/MyAotWeb.Api.http) to add:

- @MyAotWeb.Api_ContainerAddress = http://localhost:8080

…and the following `GET` operation:

```http
### local container running
GET {{MyAotWeb.Api_ContainerAddress}}/todos/
Accept: application/json
```

## publish the `ASP.NET Core Web API (native AOT)` project to the local Podman container host

From the `MyAotWeb.Api` [directory](./MyAotWeb.Api):

```bash
dotnet publish /t:PublishContainer
```

We should be able to run the `GET` operation above and see a response from our local Podman.

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

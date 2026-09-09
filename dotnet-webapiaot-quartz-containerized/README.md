# ASP.NET Core Web API (native AOT), containerized with Quartz.NET

We are just going to run `dotnet new webapiaot` where `webapiaot` is a default project template called `ASP.NET Core Web API (native AOT)` that should show up in `dotnet new list`. With that prerequisite, we are going to:

1. generate the `ASP.NET Core Web API (native AOT)` project and add a Solution file
2. change the `ASP.NET Core Web API (native AOT)` project file for containers
3. change the default `*.http` file to point at the container port
4. add the `quartz` package and schedule a TODO job
5. publish the `ASP.NET Core Web API (native AOT)` project to the local Podman container host

## generate the `ASP.NET Core Web API (native AOT)` project and add a Solution file

From the `dotnet-webapiaot-quartz-containerized` [directory](../dotnet-webapiaot-quartz-containerized):

```bash
dotnet new webapiaot -o MyAotQuartzWeb.Api

dotnet new sln -n MyAotQuartzWeb.Api
dotnet sln MyAotQuartzWeb.Api.slnx add ./MyAotQuartzWeb.Api/MyAotQuartzWeb.Api.csproj
```

## change the `ASP.NET Core Web API (native AOT)` project file for containers

Edit the `MyAotQuartzWeb.Api.csproj` [file](./MyAotQuartzWeb.Api/MyAotQuartzWeb.Api.csproj) to add:

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

Edit the `MyAotQuartzWeb.Api.http` [file](./MyAotQuartzWeb.Api/MyAotQuartzWeb.Api.http) to add:

- @MyAotQuartzWeb.Api_ContainerAddress = http://localhost:8080

…and the following `GET` operation:

```http
### local container running
GET {{MyAotQuartzWeb.Api_ContainerAddress}}/todos/
Accept: application/json
```

## add the `quartz` package and schedule a TODO job

From the `MyAotQuartzWeb.Api` [directory](./MyAotQuartzWeb.Api) run:

```bash
dotnet add package quartz
```

Add the `MyJob.cs` `class` [file](./MyAotQuartzWeb.Api/MyJob.cs) to schedule a Quartz Job.

Change the `Program.cs` `class` [file](./MyAotQuartzWeb.Api/Program.cs) to:

- load `sampleTodos` as a singleton so `MyJob` can report on it
- add `MyJob` to the DI container with `AddKeyedTransient<IJob, MyJob>`
- call `AddQuartz` and `AddQuartzHostedService` to schedule `MyJob` and register with the Host, respectively

## publish the `ASP.NET Core Web API (native AOT)` project to the local Podman container host

From the `MyAotQuartzWeb.Api` [directory](./MyAotQuartzWeb.Api):

```bash
dotnet publish /t:PublishContainer
```

We should be able to view the Podman logs and see `MyJob` running.

Also, we should be able to run the `GET` operation above and see a response from our local Podman.

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

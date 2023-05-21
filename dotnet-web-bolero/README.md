# Bolero: F# in WebAssembly

<https://fsbolero.io/>

<https://github.com/fsbolero/bolero>

## setup

```shell
dotnet new -i Bolero.Templates
```

From the `dotnet-web-bolero` [directory](../dotnet-web-bolero):

```shell
dotnet new bolero-app -o MyBolero.One
```

From the `MyBolero.One.Server` [directory](./MyBolero.One/src/MyBolero.One.Server):

```shell
dotnet run --project MyBolero.One.Server.fsproj
```

## setup (without server)

From the `dotnet-web-bolero` [directory](../dotnet-web-bolero):

```shell
dotnet new bolero-app -s=false -o MyBolero.Two
```

From the `MyBolero.Two.Client` [directory](./MyBolero.Two/src/MyBolero.Two.Client):

```shell
dotnet run --project MyBolero.Two.Client.fsproj
```

## Bolero pre-renders HTML by default

>`prerendered: bool` [passed to `AddBoleroHost`] determines whether the dynamic Bolero content is prerendered.
>
>If true, then the content is rendered on the server side (even in `WebAssembly` mode) and the resulting HTML is included in the static content served by ASP.NET Core. This prevents the “pop-in” effect where the user first sees an empty container, and once the page is ready (ie `WebAssembly` has started or SignalR has connected), the content suddenly appears.
>
>The default is `true`.
>
><https://fsbolero.io/docs/Hosting#configuring-hosted-modes>

## Bolero showcases the Bulma CSS framework

<https://bulma.io/>

@[BryanWilhite](https://twitter.com/BryanWilhite)

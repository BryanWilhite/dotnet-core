# Bolero: F# in WebAssembly

<https://fsbolero.io/>

<https://github.com/fsbolero/bolero>

## setup

```shell
dotnet new -i Bolero.Templates
```

From the [`dotnet-web-bolero`](../dotnet-web-bolero) directory:

```shell
dotnet new bolero-app -o MyBolero.One
```

From the [`MyBolero.One.Server`](./MyBolero.One/src/MyBolero.One.Server) directory:

```shell
dotnet run --project MyBolero.One.Server.fsproj
```

## Bolero pre-renders HTML by default

>`prerendered: bool` [passed to `AddBoleroHost`] determines whether the dynamic Bolero content is prerendered.
>
>If true, then the content is rendered on the server side (even in `WebAssembly` mode) and the resulting HTML is included in the static content served by ASP.NET Core. This prevents the “pop-in” effect where the user first sees an empty container, and once the page is ready (ie `WebAssembly` has started or SignalR has connected), the content suddenly appears.
>
>The default is `true`.
>
><https://fsbolero.io/docs/Hosting#configuring-hosted-modes>

## Bolero uses the Bulma CSS framework

<https://bulma.io/>

@[BryanWilhite](https://twitter.com/BryanWilhite)

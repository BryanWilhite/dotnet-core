# Bolero: F# in WebAssembly

>F# in WebAssembly
>
>Develop SPAs with the full power of F# and .NET.
>

## <https://fsbolero.io/>

- **GitHub:** <https://github.com/fsbolero/bolero>
- **lead GitHub contributor:** [Loïc Denuzière](https://github.com/Tarmil)

## setup with ASP.NET server

```shell
dotnet new -i Bolero.Templates
```

From the `dotnet-web-bolero` [directory](../dotnet-web-bolero):

```shell
dotnet new bolero-app -o MyBolero.Server
```

From the `MyBolero.Server.Server` [directory](./MyBolero.Server/src/MyBolero.Server.Server):

```shell
dotnet run --project MyBolero.Server.Server.fsproj
```

## setup (without server)

From the `dotnet-web-bolero` [directory](../dotnet-web-bolero):

```shell
dotnet new bolero-app -s=false -o MyBolero.WebAssembly
```

From the `MyBolero.WebAssembly.Client` [directory](./MyBolero.WebAssembly/src/MyBolero.WebAssembly.Client):

```shell
dotnet run --project MyBolero.WebAssembly.Client.fsproj
```

### publish `MyBolero.WebAssembly.Client`

Publishing `MyBolero.WebAssembly.Client` makes it available to any static HTML application.

From the `MyBolero.WebAssembly.Client` [directory](./MyBolero.WebAssembly/src/MyBolero.WebAssembly.Client):

```shell
dotnet publish -c Release -o bin/publish
```

## Bolero (with server) pre-renders HTML by default

>`prerendered: bool` [passed to `AddBoleroHost`] determines whether the dynamic Bolero content is prerendered.
>
>If true, then the content is rendered on the server side (even in `WebAssembly` mode) and the resulting HTML is included in the static content served by ASP.NET Core. This prevents the “pop-in” effect where the user first sees an empty container, and once the page is ready (ie `WebAssembly` has started or SignalR has connected), the content suddenly appears.
>
>The default is `true`.
>
><https://fsbolero.io/docs/Hosting#configuring-hosted-modes>

## how the client-side-only version differs from the default setup

The client-side-only (`-s=false`) Bolero project is closest to that of a classic SPA: no prerendering and no SEO possibilities.

Instead of loading data with a _Remoting_ [📖 [docs](https://fsbolero.io/docs/Remoting)] concept, `-s=false` Bolero features the plain-old .NET `HttpClient`; the `WebAssemblyHostBuilder` adds it as a [scoped service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0#overview-of-dependency-injection) in the `Startup.fs` [file](./Songhay.BoleroClientOnly/src/Songhay.BoleroClientOnly.Client/Startup.fs):

```fsharp
builder.Services.AddScoped<HttpClient>(fun _ ->
    new HttpClient(BaseAddress = Uri builder.HostEnvironment.BaseAddress)) |> ignore
```

The elmish `update` function in the `Main.fs` [file](../dotnet-web-bolero/MyBolero.WebAssembly/src/MyBolero.WebAssembly.Client/Main.fs) therefore accepts `HttpClient` instead of a type related to Bolero-server Remoting:

```fsharp
let update (http: HttpClient) message model =
    match message with
    | SetPage page ->
        { model with page = page }, Cmd.none

    | Increment ->
        { model with counter = model.counter + 1 }, Cmd.none
    | Decrement ->
        { model with counter = model.counter - 1 }, Cmd.none
    | SetCounter value ->
        { model with counter = value }, Cmd.none

    | GetBooks ->
        let getBooks() = http.GetFromJsonAsync<Book[]>("/books.json")
        let cmd = Cmd.OfTask.either getBooks () GotBooks Error
        { model with books = None }, cmd
    | GotBooks books ->
        { model with books = Some books }, Cmd.none

    | Error exn ->
        { model with error = Some exn.Message }, Cmd.none
    | ClearError ->
        { model with error = None }, Cmd.none
```

The use of `http.GetFromJsonAsync` is, again, a plain-old [.NET extension method](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.getfromjsonasync?view=net-6.0) that appeared in .NET 5.0.

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

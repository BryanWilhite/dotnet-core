# Bolero: F# in WebAssembly

<https://fsbolero.io/>

<https://github.com/fsbolero/bolero>

## setup

This setup builds on the `dotnet-web-bolero` [sample](https://github.com/BryanWilhite/dotnet-core/tree/master/dotnet-web-bolero):

From the `dotnet-web-bolero-server-false` [directory](../dotnet-web-bolero-server-false):

```shell
dotnet new bolero-app -o Songhay.BoleroClientOnly -s=false
```

From the `Songhay.BoleroClientOnly` [directory](./Songhay.BoleroClientOnly):

```shell
dotnet run
```

## how the client-side-only version differs from the default setup

The client-side-only (`-s=false`) Bolero project is closest to that of a classic SPA: no prerendering and no SEO possibilities.

Instead of loading data with a _Remoting_ [📖 [docs](https://fsbolero.io/docs/Remoting)] concept, `-s=false` Bolero features the plain-old .NET `HttpClient`; the `WebAssemblyHostBuilder` adds it as a [scoped service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0#overview-of-dependency-injection) in the `Startup.fs` [file](./Songhay.BoleroClientOnly/src/Songhay.BoleroClientOnly.Client/Startup.fs):

```fsharp
builder.Services.AddScoped<HttpClient>(fun _ ->
    new HttpClient(BaseAddress = Uri builder.HostEnvironment.BaseAddress)) |> ignore
```

The elmish `update` function therefore accepts `HttpClient` instead of a type related to Bolero-server Remoting:

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

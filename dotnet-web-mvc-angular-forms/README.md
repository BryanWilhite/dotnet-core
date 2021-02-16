# Angular Forms

The contents of [this folder](../dotnet-web-mvc-angular-forms) represents an approach to Angular over ASP.NET Core. The work here was tracked with [a single GitHub issue](https://github.com/BryanWilhite/dotnet-core/issues/20).

## `dotnet new angular`

From the `dotnet-web-mvc-angular-forms/` [folder](../dotnet-web-mvc-angular-forms), we start with `dotnet new angular`:

```bash
dotnet new angular \
    -o Songhay.AngularForms/Songhay.AngularForms

dotnet new sln \
    -n Songhay.AngularForms \
    -o Songhay.AngularForms

dotnet sln Songhay.AngularForms/Songhay.AngularForms.sln add \
    Songhay.AngularForms/Songhay.AngularForms/Songhay.AngularForms.csproj
```

As of this writing, `dotnet new angular` has these Microsoft opinions:

- install/build a Node.JS `$(SpaRoot)` from the MVC  `*.csproj` [file](./Songhay.AngularForms/Songhay.AngularForms/Songhay.AngularForms.csproj)
- call `IApplicationBuilder.UseSpa()` extension method [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.spaapplicationbuilderextensions.usespa?view=aspnetcore-3.0)] in `Startup.Configure()`
- call `IServiceCollection.AddSpaStaticFiles()` extension method [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.spastaticfilesextensions.addspastaticfiles?view=aspnetcore-3.0)] in `Startup.ConfigureServices()`
- install the typical, typescript-based Angular `npm` packages declared in the `package.json` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/package.json)

In addition to the typical Angular `npm` packages, Microsoft adds these:

| package name | dependency type
| -| -|
| `aspnet-prerendering` [📦 [npm](https://www.npmjs.com/package/aspnet-prerendering)] | `dependencies`
| `bootstrap` [📦 [npm](https://www.npmjs.com/package/bootstrap)] | `dependencies`
| `jquery` [📦 [npm](https://www.npmjs.com/package/jquery)] | `dependencies`
| `popper.js` [📦 [npm](https://www.npmjs.com/package/@skyscanner/popper.js)] | `dependencies`

## upgrading Angular (twice)

According to “[Upgrade the Angular .NET Core SPA Template to Angular 9](https://jasontaylor.dev/asp-net-core-angular-9-upgrade/)” by Jason Taylor, we can upgrade the Angular 8 defaults from Microsoft to Angular 10 by executing `ng update` (for core and CLI) twice:

```bash
cd ./Songhay.AngularForms/Songhay.AngularForms/ClientApp
ng update @angular/core@8 @angular/cli@8
```

To verify the freshness 📦✨ of these `npm` packages, I am currently fond of using `npm-check` [📦 [npm](https://www.npmjs.com/package/npm-check)]:

```bash
cd ./Songhay.AngularForms/Songhay.AngularForms/ClientApp
npm-check -u
```

As of this writing, Microsoft is using Angular 8.x (about two versions behind), so I confine myself to all updates that are _not_ major updates.

## running the default project

To run what Microsoft is giving us, we can call `dotnet build` and `dotnet run` from the `dotnet-web-mvc-angular-forms/` [folder](../dotnet-web-mvc-angular-forms):

```bash
dotnet build Songhay.AngularForms/Songhay.AngularForms.sln
dotnet run --project Songhay.AngularForms/Songhay.AngularForms/Songhay.AngularForms.csproj
```

@[BryanWilhite](https://twitter.com/BryanWilhite)

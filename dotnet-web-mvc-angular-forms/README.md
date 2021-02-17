# Angular Forms

The contents of [this folder](../dotnet-web-mvc-angular-forms) represents an approach to Angular over ASP.NET Core. The work here was tracked with [a single GitHub issue](https://github.com/BryanWilhite/dotnet-core/issues/20).

## running `dotnet new angular`

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

## using `npx` in defiance of Google

On my machines, I do not have the Angular CLI, `ng`, installed globally. I have installed `npx` [📦 [npm](https://www.npmjs.com/package/npx)] globally to reduce dragging around and maintaining tools above the repo level. This means the scripts in the `package.json` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/package.json) have been changed:

```json
"scripts": {
  "ng": "npx ng",
  "start": "npx ng serve",
  "build": "npx ng build",
  "build:ssr": "npx ng run Songhay.AngularForms:server:dev",
  "test": "npx ng test",
  "lint": "npx ng lint",
  "e2e": "npx ng e2e"
},
```

## upgrading Angular (three times) to 11.x

According to “[Upgrade the Angular .NET Core SPA Template to Angular 9](https://jasontaylor.dev/asp-net-core-angular-9-upgrade/)” by Jason Taylor, we can upgrade the Angular 8 defaults from Microsoft to Angular 10 by executing `ng update` (for core and CLI) three times:

```bash
cd ./Songhay.AngularForms/Songhay.AngularForms/ClientApp
npm i npm@6 --save-dev
npx ng update @angular/core@9 @angular/cli@9
npm run build
```

Note that I installed `npm` 6.x locally because of [an Angular CLI comment](https://github.com/angular/angular-cli/issues/19957#issuecomment-775407654) (and my unwillingness to change my global `npm` for Google).

After each update I verify that the build is working by following “running the default project” (below). There should be a build error that is mentioned and addressed in [my GitHub comment](https://github.com/BryanWilhite/dotnet-core/issues/20#issuecomment-779575700).

I notice that `ng update` complains when the repo is not “clean.” I make sure to commit before updating again:

```bash
npm un @nguniversal/module-map-ngfactory-loader
npx ng update @angular/core@10 @angular/cli@10
npm run build
```

I had to uninstall `@nguniversal/module-map-ngfactory-loader` in the commands above. This package is considered deprecated. I made [a GitHub comment](https://github.com/BryanWilhite/dotnet-core/issues/20#issuecomment-779590758) about this inconvenient complication.

Finally, the last update to Angular 11:

```bash
npm i codelyzer@latest
npx ng update @angular/core@11 @angular/cli@11
npm run build
```

For this last update, we have to touch `codelyzer` to prevent peer-dependency errors. Also, when we `run build` we should see this warning:

```console
'node-sass' usage is deprecated and will be removed in a future major version. To opt-out of the deprecated behaviour and start using 'sass' uninstall 'node-sass'
```

The warning leads to:

```bash
npm un node-sass
npm i sass --save-dev
```

This journey has been quite brutal.

## updating packages not touched by `ng update`

To verify the freshness 📦✨ of these `npm` packages, I am currently fond of using `npm-check` [📦 [npm](https://www.npmjs.com/package/npm-check)]:

```bash
npm-check -u
```

Everything was updated except for that local `npm` installed  earlier.

## running the `Songhay.AngularForms` project

To run what Microsoft and Google are giving us, we can call `dotnet build` and `dotnet run` from the `dotnet-web-mvc-angular-forms/` [folder](../dotnet-web-mvc-angular-forms):

```bash
dotnet build Songhay.AngularForms/Songhay.AngularForms.sln
dotnet run --project Songhay.AngularForms/Songhay.AngularForms/Songhay.AngularForms.csproj
```

## actually getting work done

Everything that happened after running `dotnet new angular` was supposed to be spread out over years in a 20-minute session with months between. Doing that `ng update` crap _in one day_ is the reason why [Ryan Dahl regrets quite a bit](https://www.youtube.com/watch?v=M3BM9TB-8yA).

### displaying the Angular version

Add the following HTML to line 7 of the `nav-menu.component.html` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/nav-menu/nav-menu.component.html):

```html
<span class="framework version">Angular {{clientFrameworkVersion}}</span>
```

Change the `nav-menu.component.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/nav-menu/nav-menu.component.ts) to look like this:

```typescript
import { Component, VERSION } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  clientFrameworkVersion: string;
  isExpanded = false;

  constructor() {
    this.clientFrameworkVersion = `${VERSION.major}.${VERSION.minor}.${VERSION.patch
      }`;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
```

### add the form components and service

From the `ClientApp/` [folder](./Songhay.AngularForms/Songhay.AngularForms/ClientApp):

```bash
npx ng g component \
    reactive-forms/form1 \
    --flat=false \
    --module=app \
    --project Songhay.AngularForms \
    --skip-import=false \
    --skip-tests=false \
    --style=css \
    --dry-run=true

npx ng g component \
    reactive-forms/form2 \
    --flat=false \
    --module=app \
    --project Songhay.AngularForms \
    --skip-import=false \
    --skip-tests=false \
    --style=css \
    --dry-run=true

npx ng g component \
    reactive-forms/form3 \
    --flat=false \
    --module=app \
    --project Songhay.AngularForms \
    --skip-import=false \
    --skip-tests=false \
    --style=css \
    --dry-run=true

npx ng g service \
    reactive-forms/app-data \
    --flat=false \
    --project Songhay.AngularForms \
    --skip-tests=false \
    --dry-run=true

npx ng g service \
    reactive-forms/forms-data \
    --flat=false \
    --project Songhay.AngularForms \
    --skip-tests=false \
    --dry-run=true
```

I left `--dry-run=true` on the commands above for the safety of the reader.

I know I will need three `form*` components because this layout is loosely based on a [StackBlitz experiment](https://stackblitz.com/edit/akita-with-reactive-forms-and-navigation) of mine.

We need two service files because `app-data.service.ts` is based on my `BehaviorSubject` [store](https://github.com/BryanWilhite/songhay-ng-workspace/blob/master/songhay/projects/songhay/core/src/lib/services/app-data.store.ts) and will serve as the base class of `forms-data.service.ts`.

### use `json-server` to drive the form at design time

We are going to install `json-server` and run through the “[Getting started](https://github.com/typicode/json-server#getting-started)” section in their repo using Karma-Jasmine testing.

Install `json-server` [🐙🐈 [GitHub](https://github.com/typicode/json-server)] from the `ClientApp/` [folder](./Songhay.AngularForms/Songhay.AngularForms/ClientApp):

```bash
npm i json-server --save-dev
```

@[BryanWilhite](https://twitter.com/BryanWilhite)

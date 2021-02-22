# Angular Forms

The contents of [this folder](../dotnet-web-mvc-angular-forms) represents an approach to Angular over ASP.NET Core. The work here was tracked with [a single GitHub issue](https://github.com/BryanWilhite/dotnet-core/issues/20) which is another way of saying I am lazy.

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

On my machines, I do not have the Angular CLI, `ng`, installed globally. I have installed `npx` [📦 [npm](https://www.npmjs.com/package/npx)] globally to reduce the need to maintain tools above the repo level. This means the scripts in the `package.json` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/package.json) have been changed:

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

According to “[Upgrade the Angular .NET Core SPA Template to Angular 9](https://jasontaylor.dev/asp-net-core-angular-9-upgrade/)” by Jason Taylor, we can upgrade the Angular 8 defaults from Microsoft to Angular 11 by executing `ng update` (for core and CLI) _three_ miserable times:

```bash
cd ./Songhay.AngularForms/Songhay.AngularForms/ClientApp
npm i npm@6 --save-dev
npx ng update @angular/core@9 @angular/cli@9
npm run build
```

Note that I installed `npm` 6.x locally because of [an Angular CLI comment](https://github.com/angular/angular-cli/issues/19957#issuecomment-775407654) (and my unwillingness to change my global `npm` for world peace between Google and Node folks).

After each update, I verify that the build is working by following “running the default project” (below). There should be a build error that is mentioned and addressed in [my GitHub comment](https://github.com/BryanWilhite/dotnet-core/issues/20#issuecomment-779575700).

I notice that `ng update` complains when the repo is not “clean.” I make sure to commit before updating again:

```bash
npm un @nguniversal/module-map-ngfactory-loader
npx ng update @angular/core@10 @angular/cli@10
npm run build
```

I had to uninstall `@nguniversal/module-map-ngfactory-loader` in the commands above. This package is considered deprecated (but I suspect SSR features depend on it). I made [a GitHub comment](https://github.com/BryanWilhite/dotnet-core/issues/20#issuecomment-779590758) about this inconvenient complication.

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

### install Akita to get the equivalent of a `BehaviorSubject` data store

I am currently under the impression that [Akita](https://github.com/datorama/akita) can _partially_ replace my beloved `BehaviorSubject` [store](https://github.com/BryanWilhite/songhay-ng-workspace/blob/master/songhay/projects/songhay/core/src/lib/services/app-data.store.ts) (see “[flippant remarks about BehaviorSubject](http://songhayblog.azurewebsites.net/entry/2019-02-25-flippant-remarks-about-behaviorsubject/)”).

From the `ClientApp/` [folder](./Songhay.AngularForms/Songhay.AngularForms/ClientApp):

```bash
npm i @datorama/akita
```

### add formly, the form components and service

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
    reactive-forms/state/forms-data \
    --flat=true \
    --project Songhay.AngularForms \
    --skip-tests=false \
    --dry-run=true
```

I left `--dry-run=true` on the commands above for the safety of the reader.

I know I will need three `form*` components because this layout is loosely based on a [StackBlitz experiment](https://stackblitz.com/edit/akita-with-reactive-forms-and-navigation). We are going to take steps beyond this experiment which leads to the installation of `formly`:

```bash
npx ng add \
  @ngx-formly/schematics \
  --ui-theme=bootstrap
```

We are now in position to re-factor the form components to use formly, but before we get on to that, we need to route to these new components. We add the form `routes` in `RouterModule.forRoot` (in the `app.module.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/app.module.ts)):

```typescript
{ path: 'wizard', component: Form1Component },
{ path: 'wizard/step-2-of-3', component: Form2Component },
{ path: 'wizard/step-3-of-3', component: Form3Component },
```

These new routes allow us to add a new `nav-item` to the `nav-menu.component.html` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/nav-menu/nav-menu.component.html):

```html
<li class="nav-item" [routerLinkActive]="['link-active']">
  <a class="nav-link text-dark" [routerLink]="['/wizard']"
    >Form Wizard</a
  >
</li>
```

Finally, the `router.navigate` commands in the form components have to be updated to recognize these new routes. See [the relevant GitHub commit](https://github.com/BryanWilhite/dotnet-core/commit/a92e80828dae278ec3eb8157e14a451e24bad49d) for details.

### replacing `FormBuilder`, `FormGroup`, `FormArray` and form HTML markup with `FormlyFieldConfig`

The installation of `@ngx-formly/schematics` in the previous step adds `@ngx-formly/core` and `@ngx-formly/bootstrap`. This allows us to eliminate the imperative use of `FormBuilder`, `FormGroup` and , `FormArray` in the form components. It also dramatically eliminates the need to declare so much HTML. This transformation is captured in [a GitHub commit](https://github.com/BryanWilhite/dotnet-core/commit/4c3bf7fad7d875e3f6e7aeafbe8853053f7897f2).

We see that we have added a new component, `RepeatPhoneTypeComponent` (in the `repeat-phone.type.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/reactive-forms/form2/repeat-phone.type.ts)), which highlights the one of the complexities of formly: implementing [a repeating section](https://formly.dev/examples/advanced/repeating-section) often calls for a `Repeat*Component` Angular component. In my opinion, formly repeating sections are not as complex as handling `FormArray` markup explicitly.

For me, the toughest part of the formly learning curve is in the realm of [validation](https://formly.dev/guide/validation). What I am seeing in formly (as of this writing) are:

- formly built-in validators (in the `form3.component.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/reactive-forms/form3/form3.component.ts))
- defining and injecting [custom validation messages on the Angular module level](https://formly.dev/examples/validation/validation-message) (in the `app.module.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/app.module.ts))
- defining [custom validation on the Angular component level](https://formly.dev/examples/validation/custom-validation) (also in the `form3.component.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/reactive-forms/form3/form3.component.ts))
- using Angular `Validators` functions to add custom validation (in the `form1.component.ts` [file](./Songhay.AngularForms/Songhay.AngularForms/ClientApp/src/app/reactive-forms/form1/form1.component.ts))

Here is what I have today for using Angular `Validators` functions:

```typescript
emailField.validators = {
  email: {
    expression: (c: AbstractControl) => {
      const errors = Validators.email(c) as { email: boolean };
      return !errors?.email ?? false;
    },
    message: (error: any, field: FormlyFieldConfig) => `"${field.formControl.value}" 
is not a valid email address`,
  },
};
```

I know the `return` statement with `!errors?.email ?? false` looks quite strange but that is my current workaround for the fact that `Validators` functions return a `ValidationErrors` object (used like a .NET Dictionary) instead of a Boolean.

What is important here is the transfer of responsibility for custom validation to the client side. The `FormlyFieldConfig` array is not concerned with this custom-validation stuff which (to me) positions it for converting it to JSON.

The `FormlyFieldConfig` array is the component property `fields` which is used to set `emailField` (above):

```typescript
const emailField = this.fields.find(field => field.key === 'email');
if (!emailField) {
  console.error('The expected formly field is not here.');
  return;
}
```

At minimum, `formly-form` requires its `fields` property be set along with `form` and `model`:

```html
<formly-form [form]="componentForm" [fields]="fields" [model]="model"></formly-form>
```

I mentioned earlier that formly eliminates the ‘imperative use’ of `FormGroup` but I lied. In order for the `[form]="componentForm"` binding to work, we need this one line of imperative code:

```typescript
componentForm = new FormGroup({});
```

For `[model]="model"`, we see that I am defining `model: Partial<ReactiveFormModel>;` and setting it like this:

```typescript
const state = this.reactiveFormService.getStateOfStore();
this.model = {
  password: state?.password,
  age: state?.age,
};
```

An error is thrown when a straightforward `this.model = state;` statement is made. I think that has to do with Akita magic: by design, Akita wants total control over the mutation of the store. This mutation is handled by calling `updateBackingStore` in a pattern like this:

```typescript
const sub = this.componentForm.valueChanges.subscribe(change => {
  if (!this.componentForm.valid) {
    return;
  }
  this.reactiveFormService.updateBackingStore(change);
});
this.subscriptions.push(sub);
```

The subscription to the `valueChanges` observable calls `updateBackingStore` too many times (in spite of the `!this.componentForm.valid` logic gate). I think some RxJs finesse is needed to throttle things down a bit but I am not going to do this now because, as stated previously, I am lazy.

### use `json-server` to drive the form at design time

We are going to install `json-server` and run through the “[Getting started](https://github.com/typicode/json-server#getting-started)” section in their repo using Karma-Jasmine testing.

Install `json-server` [🐙🐈 [GitHub](https://github.com/typicode/json-server)] from the `ClientApp/` [folder](./Songhay.AngularForms/Songhay.AngularForms/ClientApp):

```bash
npm i --save-dev \
  npm-run-all \
  json-server
```

As of this writing, formly offers two ways to use JSON:

1. emit a stringified (serialized) form of `FormlyFieldConfig[]` [[example](https://formly.dev/examples/other/json-powered)]
2. emit formal JSON Schema that can be converted to `FormlyFieldConfig[]` with `FormlyJsonschema.toFieldConfig()` [[example](https://formly.dev/examples/advanced/json-schema)]

Today we choose option _1_.

@[BryanWilhite](https://twitter.com/BryanWilhite)

# ASP.NET MVC validation with jQuery AJAX

This folder contains the various ways [ASP.NET Core](https://en.wikipedia.org/wiki/ASP.NET_Core) supports validation with jQuery AJAX. This represents my public recognition that jQuery [[GitHub](https://github.com/jquery)] (by the [OpenJS Foundation](https://openjsf.org/)) _still_ plays a default role in the surfacing of .NET validation solutions to the the Web. Since the turn of the century, jQuery has been accompanied by:

- jQuery Validation by Jörn Zaefferer
- jQuery Unobtrusive Validation [[GitHub](https://github.com/aspnet/jquery-validation-unobtrusive#jquery-unobtrusive-validation)] by the .NET Foundation

## historical jQuery validation articles

These historical jQuery validation articles may select the background which might be relevant to ASP.NET (Core) in the .NET 6 time frame:

- Document unobtrusive validation #1111 [[GitHub](https://github.com/dotnet/AspNetCore.Docs/issues/1111)]
- `jquery-ajax-unobtrusive` documentation [[StackOverflow](https://stackoverflow.com/a/50148838/22944) and [StackOverflow](https://stackoverflow.com/a/15977785/22944)] (see “[Unobtrusive validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#unobtrusive-validation)”)
- “[Setting up jQuery Unobtrusive Validation](https://www.mobzystems.com/blog/setting-up-jquery-unobtrusive-validation/)”
- “[Applying unobtrusive jquery validation to dynamic content in ASP.Net MVC](https://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/)”
- “[ASP.NET MVC 3 unobtrusive jQuery client-side validation with child collections](https://stackoverflow.com/questions/7015526/asp-net-mvc-3-unobtrusive-jquery-client-side-validation-with-child-collections)”

The first links in the list above are suggesting that StackOverflow is being used as a documentation location for jQuery validation technologies which I will perpetually find unusual.

### the most important bits from the links above

Once jQuery validation files are ‘wired up’ (which is conventionally done by referencing the partial, `Views/Shared/_ValidationScriptsPartial.cshtml`, generated via `dotnet new mvc`), we activate client-side validation by adding the following attribute to the `form` element: `data-ajax=true`.

What remains is the application of the attributes used for “unobtrusive” validation. My work here will show that it can be done auto-magically by Microsoft “helpers” or declared in markup manually by hand (see “three approaches to validation with jQuery AJAX” below).

### there is no `System.Web.Mvc.Ajax` in the world of .NET Core

The `System.Web.Mvc.Ajax` namespace contained helpers like `AjaxExtensions.BeginForm` [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.ajax.ajaxextensions.beginform?view=aspnet-mvc-5.2)] which might be carried around in the `MicrosoftMvcAjax.Mvc5` [NuGet package](https://www.nuget.org/packages/MicrosoftMvcAjax.Mvc5/). I assume this work is not continued and not intended for the world of .NET Core because of the dependency on jQuery libraries—and jQuery itself has been regarded as a legacy technology for over a decade. A relatively recent article on this subject is “[ASP.NET MVC 5 - Razor AJAX Form Control](https://www.c-sharpcorner.com/article/asp-net-mvc5-razor-ajax-form-control/).”

## three approaches to validation with jQuery AJAX

I have here three approaches to validation with jQuery AJAX:

1. declaring in markup manually by hand and `Html.EditorFor`
2. using `Html.EditorFor` and Data Annotations
3. repeating #2 with FluentValidation [[GitHub](https://github.com/FluentValidation/FluentValidation)] instead of Data Annotations

The following links provide the background for this work:

- “The Form Action Tag Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0#the-form-action-tag-helper)]
- “Using DataType Attributes” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-6.0#using-datatype-attributes)]
- “`HtmlHelperEditorExtensions.EditorFor` Method” [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.rendering.htmlhelpereditorextensions.editorfor?view=aspnetcore-6.0)]
- “Asynchronous HTML Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-6.0#asynchronous-html-helper)]
- “[Multiple ViewModels in a single MVC View](https://damienbod.com/2014/01/27/multiple-viewmodels-in-a-single-mvc-view/)”
- “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)”
- “[ASP.NET MVC EditorTemplate sub folders](https://stackoverflow.com/questions/21945426/asp-net-mvc-editortemplate-sub-folders)”

## sample set up

From the `dotnet-web-mvc-validation/` [directory](../dotnet-web-mvc-validation):

```bash
dotnet new sln -n Songhay.Validation
```

For approach #1:

```bash
dotnet new mvc -o Songhay.ValidationWithMarkup.Web

dotnet sln Songhay.Validation.sln \
      add Songhay.ValidationWithMarkup.Web/Songhay.ValidationWithMarkup.Web.csproj
```

For approach #2:

```bash
dotnet new mvc -o Songhay.ValidationWithAnnotations.Web

dotnet sln Songhay.Validation.sln \
      add Songhay.ValidationWithAnnotations.Web/Songhay.ValidationWithAnnotations.Web.csproj
```

For approach #3:

```bash
dotnet new mvc -o Songhay.FluentValidation.Web

dotnet sln Songhay.Validation.sln \
      add Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj
```

All three approaches will use the same, famous models: a `TodoList` which contains a list of `TodoItem`.

## `Html.EditorFor` and `Html.PartialAsync`

Because we are using a `TodoList` which contains a list of `TodoItem` we _must_ use `Html.EditorFor` which can be considered a perceived performance loss because we effectively _must_ load a potentially large Web page _synchronously_, losing the benefits of `Html.PartialAsync`. This list of `TodoItem` is a _child_ collection that must be indexed in order to meet validation conventions. “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)” details these conventions.

When it is possible to use `Html.PartialAsync`, the caveat here is to avoid using markup like this:

```html
<div asp-validation-summary="All" class="text-danger"></div>
```

My work is showing me that we should prefer the following instead:

```csharp
@Html.ValidationSummary(false, "", new { @class = "text-danger" })
```

Additionally, we should prefer using `@Html.ValidationMessage` [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.html.validationextensions.validationmessage?view=aspnet-mvc-5.2)] or `@Html.ValidationMessageFor` [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.html.validationextensions.validationmessagefor?view=aspnet-mvc-5.2)] over this:

```html
<span asp-validation-for="Name" class="text-danger"></span>
```

These preferences of mine should be considered when the HTML declared inside a partial is ignored by jQuery validation.

## related links

- [according to Wikipedia](https://en.wikipedia.org/wiki/ASP.NET_MVC), the final release of ASP.NET MVC was 2022-04-12
- [jqueryvalidation.org](https://jqueryvalidation.org/)
- Form Helper [[GitHub](https://github.com/sinanbozkus/FormHelper)] [[Form Helper sample project](https://github.com/sinanbozkus/fluent-validation-with-form-helper/tree/master/StudentProject)]

@[BryanWilhite](https://twitter.com/BryanWilhite)

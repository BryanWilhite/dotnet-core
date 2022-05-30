# ASP.NET MVC unobtrusive validation with jQuery AJAX

This directory contains three ways [ASP.NET Core](https://en.wikipedia.org/wiki/ASP.NET_Core) supports unobtrusive validation with jQuery AJAX. This represents my public recognition that jQuery [[GitHub](https://github.com/jquery)] (by the [OpenJS Foundation](https://openjsf.org/)) _still_ plays a default role in the surfacing of .NET validation solutions to the the Web. Since the turn of the century, jQuery has been accompanied by:

- jQuery Validation by Jörn Zaefferer [📖 [docs](https://jqueryvalidation.org/documentation/)]
- jQuery Unobtrusive Validation [[GitHub](https://github.com/aspnet/jquery-validation-unobtrusive#jquery-unobtrusive-validation)] by the .NET Foundation

The prominent warning here is that it is an error to assume that jQuery Validation and jQuery Unobtrusive Validation must work together. The opinion here based on experience is that we should start and likely stay with jQuery Validation and _never use jQuery Unobtrusive Validation_. This very strong recommendation is explored in another sample here, in the `dotnet-web-mvc-validation/` [directory](../dotnet-web-mvc-validation).

Nevertheless, in order for jQuery Unobtrusive Validation to be _unobtrusive_ there is a high cost in conventions over customization. To research these conventions, see “unobtrusive validation articles” below:

## unobtrusive validation articles

- Document unobtrusive validation #1111 [[GitHub](https://github.com/dotnet/AspNetCore.Docs/issues/1111)]
- `jquery-ajax-unobtrusive` documentation [[StackOverflow](https://stackoverflow.com/a/50148838/22944) and [StackOverflow](https://stackoverflow.com/a/15977785/22944)] (see “[Unobtrusive validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#unobtrusive-validation)”)
- “[Setting up jQuery Unobtrusive Validation](https://www.mobzystems.com/blog/setting-up-jquery-unobtrusive-validation/)”
- “[Applying unobtrusive jquery validation to dynamic content in ASP.Net MVC](https://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/)”
- “[ASP.NET MVC 3 unobtrusive jQuery client-side validation with child collections](https://stackoverflow.com/questions/7015526/asp-net-mvc-3-unobtrusive-jquery-client-side-validation-with-child-collections)”

The first links in the list above are suggesting that StackOverflow is being used as a documentation location for unobtrusive jQuery validation which I will perpetually find unusual.

### the most important bits from the links above

Once jQuery validation files are ‘wired up’ (which is conventionally done by referencing the partial, `Views/Shared/_ValidationScriptsPartial.cshtml`, generated via `dotnet new mvc`), we activate _unobtrusive_ validation by adding the following attribute to the `form` element: `data-ajax=true`.

What remains is the application of the attributes used for unobtrusive validation. My work here will show that it can be done auto-magically by Microsoft “helpers” or declared in markup manually by hand (see “three approaches to validation with jQuery AJAX” below).

## three approaches to unobtrusive validation with jQuery AJAX

I have here three approaches to unobtrusive validation with jQuery AJAX:

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

### to benefit from unobtrusive validation, prefer `Html.EditorFor` over `Html.PartialAsync`

Because we are using a `TodoList` which contains a list of `TodoItem` we _must_ use `Html.EditorFor` which can be considered a perceived performance loss because we effectively _must_ load a potentially large Web page with many, many partials _synchronously_<sup>1</sup>, losing the benefits of `Html.PartialAsync`. This list of `TodoItem` is a _child_ collection that must be indexed in order to meet unobtrusive validation conventions. “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)” details these conventions.

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

___

<sup>1</sup> <small>The `EditorFor<TResult>` [method](https://github.com/dotnet/aspnetcore/blob/c85baf8db0c72ae8e68643029d514b2e737c9fae/src/Mvc/Mvc.ViewFeatures/src/HtmlHelperOfT.cs#L192) calls the `GenerateEditor` [method](https://github.com/dotnet/aspnetcore/blob/f0c7d0b7fea0c94b362af6579ce45928c8421846/src/Mvc/Mvc.ViewFeatures/src/HtmlHelper.cs#L897) which returns an instance of the internal `TemplateBuilder` [class](https://github.com/dotnet/aspnetcore/blob/f0c7d0b7fea0c94b362af6579ce45928c8421846/src/Mvc/Mvc.ViewFeatures/src/TemplateBuilder.cs#L14), building a `TemplateRenderer` which features a `Render` method, using a [render Task](https://github.com/dotnet/aspnetcore/blob/f0c7d0b7fea0c94b362af6579ce45928c8421846/src/Mvc/Mvc.ViewFeatures/src/TemplateRenderer.cs#L139) that is made synchronous with the old, `.GetAwaiter().GetResult()` business.</small>

## adding a new row on the client side is adding “dynamic content”

“[Applying unobtrusive jquery validation to dynamic content in ASP.Net MVC](https://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/)” details how to add a new row without a post-back by extending `$.validator.unobtrusive`. This exploration introduces me to one of two techniques of using `$.validator.unobtrusive` directly. The second one is far less complex, featuring a pattern like this:

```javascript
if($.validator) {
    $.validator.parse('.row.insert');

    if($(#MyForm).validate) {
        $(#MyForm).validate();
        if(!$(#MyForm).valid()) { return; }
    }
}
```

This simple script shows us that:

- `$.validator.parse` can be called to validate a subset of an entire form
- nothing interactive happens after parsing until `$(#MyForm).validate()` is called
- `$(#MyForm).valid()` can be used as a logic gate to prevent a post-back or an AJAX call

When we are adding a row, then our ‘subset of an entire form’ represents that new row submission. When this new row submission is validated on the client side then, our add-new script often performs a post-back to a Controller Action that validates it again on the server side and returns a Partial which appends itself to the entire form. This new data is not really saved in a datastore until the entire form is submitted.

See “Steve Sanderson’s `Html.BeginCollectionItem`” below to investigate how the Partial is used when adding to a child collection.

## Steve Sanderson’s `Html.BeginCollectionItem`

“[Editing a variable length list, ASP.NET MVC 2-style](http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/)” is the oft cited Steve Sanderson Blog post introducing us to:

>…a HTML helper I made that you can use when rendering a sequence of items that should later be model bound to a single collection.
>
>—Steve Sanderson

The eventually needed **Add New** button for adding to a child collection of items would depend on this Helper. In the ASP.NET Core time frame, Sanderson’s code (now [under Dan Ludwig](https://github.com/danludwig/BeginCollectionItem)) cannot work. BeginCollectionItemCore [[GitHub](https://github.com/saad749/BeginCollectionItemCore)] promises to be a faithful port to ASP.NET Core. “[MVC Series Part 1: Dynamically Adding Items Part 1](https://jasonco.org/2015/04/08/mvc-series-part-1-dynamically-adding-items-part-1/)” also promises to be the Blog series to detail how to use `Html.BeginCollectionItem` in 2015 which could be ‘close enough’ to ASP.NET Core.

## sample set up

From the `dotnet-web-mvc-unobtrusive-validation/` [directory](../dotnet-web-mvc-unobtrusive-validation):

```bash
dotnet new sln -n Songhay.UnobtrusiveValidation
```

For approach #1:

```bash
dotnet new mvc -o Songhay.ValidationWithMarkup.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.ValidationWithMarkup.Web/Songhay.ValidationWithMarkup.Web.csproj
```

For approach #2:

```bash
dotnet new mvc -o Songhay.ValidationWithAnnotations.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.ValidationWithAnnotations.Web/Songhay.ValidationWithAnnotations.Web.csproj
```

For approach #3:

```bash
dotnet new mvc -o Songhay.FluentValidation.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj
```

All three approaches will use the same, famous models: a `TodoList` which contains a list of `TodoItem`.

@[BryanWilhite](https://twitter.com/BryanWilhite)

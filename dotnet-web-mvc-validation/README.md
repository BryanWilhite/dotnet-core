# ASP.NET MVC validation with jQuery AJAX

This directory represents the recommended way [ASP.NET Core](https://en.wikipedia.org/wiki/ASP.NET_Core) should support validation with jQuery Validation by Jörn Zaefferer [📖 [docs](https://jqueryvalidation.org/documentation/)]. After decades of avoiding this by working with SPAs, this sample represents my public recognition that jQuery [[GitHub](https://github.com/jquery)] (by the [OpenJS Foundation](https://openjsf.org/)) _still_ plays a default role in the surfacing of .NET validation solutions to the the Web.

## recommendations

On the client side:

- understand that [HTML5 validation](https://developer.mozilla.org/en-US/docs/Learn/Forms/Form_validation) should be at the core of our design thinking
- leverage [Bootstrap validation](https://getbootstrap.com/docs/5.1/forms/validation/) UX in harmony with the MVC defaults
- consider calling `$(control).valid()` on individual form elements for maximum flexibility of form design
- prefer `Html.PartialAsync` over `Html.EditorFor` (which is only required for [unobtrusive validation](../dotnet-web-mvc-unobtrusive-validation))

On the server side:

- use FluentValidation [[GitHub](https://github.com/FluentValidation/FluentValidation)] (and FluentValidation.AspNetCore [[GitHub](https://github.com/FluentValidation/FluentValidation)]) with static methods in classes implementing `AbstractValidator<T>` that emit form validation attributes
- test `AbstractValidator<T>` classes with help from Bogus [[GitHub](https://github.com/bchavez/Bogus)] and AutoBogus [[GitHub](https://github.com/nickdodd79/AutoBogus)]
- prevent unnecessary server-side validation (and redundantly saving data) with Compare-Net-Objects [[GitHub](https://github.com/GregFinzer/Compare-Net-Objects)]

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

## other validation-related notes

### the importance of calling `ModelState.Clear()`

When validation errors show up in the client _after_ a post-back, this means ASP.NET validations conventions are working correctly, reading the contents of `Controller.ModelState` [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.controller.modelstate?view=aspnet-mvc-5.2)]. When these errors are unexpected, this is likely a side effect of server-side validation being out of sync with client-side validation. This likely happens when the model passed to the controller method is _not_ the model that is saved to the data store. `Controller.ModelState` is bound to the data passed to the controller which can be deemed irrelevant after it is processed in some kind of server-side transformation. When such a transformation is considered successful, then we should call `ModelState.Clear()` to prevent unexpected validation errors after post-back.

### `enum` and the `select` element

I must not forget about the `@Html.GetEnumSelectList` method [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.viewfeatures.htmlhelper.getenumselectlist?view=aspnetcore-6.0)]. This HTML Helper turns an `enum` into a dropdown list as described in “[ASP.NET Core - how to bind enum to HTML select tag](https://kmatyaszek.github.io/2019/10/27/asp.net-core-how-to-bind-enum-to-html-select-tag.html).”

A StackOverflow [answer](https://stackoverflow.com/a/53742229/22944) shows what we might end up with:

```html
<select asp-for="Status" 
        asp-items="Html.GetEnumSelectList<EnumStatus>()" 
        class="form-control>
    <option selected="selected" value="">Please select</option>
</select>
```

And, in case there are edge cases where we cannot use this Helper, we can resort to something like [this](https://stackoverflow.com/a/55506887/22944):

```csharp
<select class="custom-select" asp-for="ValueType.Controller">
@foreach (var e in Enum.GetValues(typeof(ValueTypeModel.PageType)).Cast<int>())
{
    if (Enum.GetName(typeof(ValueTypeModel.PageType), e) == Model.ValueType.Controller)
    {
        <option value="@e.ToString()" selected>@Enum.GetName(typeof(ValueTypeModel.PageType), e)</option>
    }
    else
    {
        <option value="@e.ToString()">@Enum.GetName(typeof(ValueTypeModel.PageType), e)</option>
    }
}
</select>
```

### there is no `System.Web.Mvc.Ajax` in the world of .NET Core

The `System.Web.Mvc.Ajax` namespace contained helpers like `AjaxExtensions.BeginForm` [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.ajax.ajaxextensions.beginform?view=aspnet-mvc-5.2)] which might be carried around in the `MicrosoftMvcAjax.Mvc5` [NuGet package](https://www.nuget.org/packages/MicrosoftMvcAjax.Mvc5/). I assume this work is not continued and not intended for the world of .NET Core because of the dependency on jQuery libraries—and jQuery itself has been regarded as a legacy technology for over a decade. A relatively recent article on this subject is “[ASP.NET MVC 5 - Razor AJAX Form Control](https://www.c-sharpcorner.com/article/asp-net-mvc5-razor-ajax-form-control/).”

## sample set up

From the `dotnet-web-mvc-validation/` [directory](../dotnet-web-mvc-validation):

```bash
dotnet new sln -n Songhay.Validation
dotnet new mvc -o Songhay.Validation.Web
dotnet sln Songhay.Validation.sln \
      add Songhay.Validation.Web/Songhay.Validation.Web.csproj

dotnet add \
    Songhay.Validation.Web/Songhay.Validation.Web.csproj \
    package FluentValidation

dotnet add \
    Songhay.Validation.Web/Songhay.Validation.Web.csproj \
    package FluentValidation.AspNetCore

touch Songhay.Validation.Web/Controllers/TodosController.cs

touch Songhay.Validation.Web/Models/ITodosContext.cs
touch Songhay.Validation.Web/Models/TodoItem.cs
touch Songhay.Validation.Web/Models/TodoItemValidator.cs
touch Songhay.Validation.Web/Models/TodoList.cs
touch Songhay.Validation.Web/Models/TodoListValidator.cs
touch Songhay.Validation.Web/Models/TodosContext.cs

mkdir Songhay.Validation.Web/Views/Todos
touch Songhay.Validation.Web/Views/Todos/Index.cshtml
touch Songhay.Validation.Web/Views/Todos/Edit.cshtml
touch Songhay.Validation.Web/Views/Todos/_TodoItem.cshtml
```

## related links

- [according to Wikipedia](https://en.wikipedia.org/wiki/ASP.NET_MVC), the final release of ASP.NET MVC was 2022-04-12
- [jqueryvalidation.org](https://jqueryvalidation.org/)
- Form Helper [[GitHub](https://github.com/sinanbozkus/FormHelper)] [[Form Helper sample project](https://github.com/sinanbozkus/fluent-validation-with-form-helper/tree/master/StudentProject)]

@[BryanWilhite](https://twitter.com/BryanWilhite)

# ASP.NET MVC validation with jQuery AJAX

This folder contains the various ways ASP.NET MVC supports validation with jQuery AJAX.

```bash
dotnet new sln -n Songhay.Validation
```

```bash
dotnet new classlib -o Songhay.Validation.Models

dotnet sln Songhay.Validation.sln \
      add Songhay.Validation.Models/Songhay.Validation.Models.csproj
```

Then we add models manually:

- `TodoList`
- `TodoItem`

```bash
dotnet new mvc -o Songhay.ValidationWithAjax.Web

dotnet add \
    Songhay.ValidationWithAjax.Web/Songhay.ValidationWithAjax.Web.csproj \
    reference Songhay.Validation.Models/Songhay.Validation.Models.csproj

dotnet sln Songhay.Validation.sln \
      add Songhay.ValidationWithAjax.Web/Songhay.ValidationWithAjax.Web.csproj
```

## related links

- “The Form Action Tag Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0#the-form-action-tag-helper)]
- jQuery Unobtrusive Validation [[GitHub](https://github.com/aspnet/jquery-validation-unobtrusive#jquery-unobtrusive-validation)]
- Document unobtrusive validation #1111 [[GitHub](https://github.com/dotnet/AspNetCore.Docs/issues/1111)]
- `jquery-ajax-unobtrusive` documentation [[StackOverflow](https://stackoverflow.com/a/50148838/22944) and [StackOverflow](https://stackoverflow.com/a/15977785/22944)] (see “[Unobtrusive validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#unobtrusive-validation)”)
- “Using DataType Attributes” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-6.0#using-datatype-attributes)]
- “`HtmlHelperEditorExtensions.EditorFor` Method” [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.rendering.htmlhelpereditorextensions.editorfor?view=aspnetcore-6.0)]
- “Asynchronous HTML Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-6.0#asynchronous-html-helper)]
- “[Setting up jQuery Unobtrusive Validation](https://www.mobzystems.com/blog/setting-up-jquery-unobtrusive-validation/)”
- “[Applying unobtrusive jquery validation to dynamic content in ASP.Net MVC](https://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/)”
- “[ASP.NET MVC 3 unobtrusive jQuery client-side validation with child collections](https://stackoverflow.com/questions/7015526/asp-net-mvc-3-unobtrusive-jquery-client-side-validation-with-child-collections)”
- “[Multiple ViewModels in a single MVC View](https://damienbod.com/2014/01/27/multiple-viewmodels-in-a-single-mvc-view/)”
- “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)”

@[BryanWilhite](https://twitter.com/BryanWilhite)

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
- Document unobtrusive validation #1111 [[GitHub](https://github.com/dotnet/AspNetCore.Docs/issues/1111)]
- `jquery-ajax-unobtrusive` documentation [[StackOverflow](https://stackoverflow.com/a/50148838/22944)] (see “[Unobtrusive validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#unobtrusive-validation)”)
- “Using DataType Attributes” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-6.0#using-datatype-attributes)]
- “`HtmlHelperEditorExtensions.EditorFor` Method” [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.rendering.htmlhelpereditorextensions.editorfor?view=aspnetcore-6.0)]
- “Asynchronous HTML Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-6.0#asynchronous-html-helper)]

@[BryanWilhite](https://twitter.com/BryanWilhite)

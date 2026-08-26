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

1. declaring in markup manually by hand and using `Html.EditorFor`
2. using `Html.EditorFor` and Data Annotations
3. repeating #2 with FluentValidation [[GitHub](https://github.com/FluentValidation/FluentValidation)] instead of Data Annotations

The following links provide the background for this work:

- “The Form Action Tag Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-8.0#the-form-action-tag-helper)]
- “Using DataType Attributes” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-8.0#using-datatype-attributes)]
- “`HtmlHelperEditorExtensions.EditorFor` Method” [📖 [docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.rendering.htmlhelpereditorextensions.editorfor?view=aspnetcore-8.0)]
- “Asynchronous HTML Helper” [📖 [docs](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-8.0#asynchronous-html-helper)]
- “[Multiple ViewModels in a single MVC View](https://damienbod.com/2014/01/27/multiple-viewmodels-in-a-single-mvc-view/)”
- “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)”
- “[ASP.NET MVC EditorTemplate sub directories](https://stackoverflow.com/questions/21945426/asp-net-mvc-editortemplate-sub-directories)”

## sample set up

The sample we are going to setup is for the classic TODO list. From the `dotnet-web-mvc-unobtrusive-validation/` [directory](../dotnet-web-mvc-unobtrusive-validation), make our Solution file:

```bash
dotnet new sln -n Songhay.UnobtrusiveValidation
```

From the `dotnet-web-mvc-unobtrusive-validation/` [directory](../dotnet-web-mvc-unobtrusive-validation) share our TODO models:

```bash
dotnet new classlib -o Songhay.Todo

del ./Songhay.Todo/Class1.cs

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add ./Songhay.Todo/Songhay.Todo.csproj
```

After the `classlib` project is generated, its `/dotnet-web-mvc-unobtrusive-validation/Songhay.Todo/Models` [directory](./dotnet-web-mvc-unobtrusive-validation/Songhay.Todo/Models) should have the following TODO-model files (I have already done this for you):

```console
Songhay.Todo/Models
├── ITodosContext.cs
├── TodoItem.cs
├── TodoList.cs
└── TodosContext.cs
```

### approach #1 (declaring in markup manually by hand and using `Html.EditorFor`)

From the `dotnet-web-mvc-unobtrusive-validation/` [directory](../dotnet-web-mvc-unobtrusive-validation):

```bash
dotnet new mvc -o Songhay.ValidationWithMarkup.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.ValidationWithMarkup.Web/Songhay.ValidationWithMarkup.Web.csproj

dotnet add \
    Songhay.ValidationWithMarkup.Web/Songhay.ValidationWithMarkup.Web.csproj \
    reference ./Songhay.Todo/Songhay.Todo.csproj

touch Songhay.ValidationWithMarkup.Web/Controllers/TodosController.cs

mkdir Songhay.ValidationWithMarkup.Web/Views/Todos
mkdir Songhay.ValidationWithMarkup.Web/Views/Todos/EditorTemplates
touch Songhay.ValidationWithMarkup.Web/Views/Todos/EditorTemplates/TodoItem.cshtml
touch Songhay.ValidationWithMarkup.Web/Views/Todos/Edit.cshtml
touch Songhay.ValidationWithMarkup.Web/Views/Todos/Index.cshtml
```

The use of the bash `touch` command to generate blank text files is an admission that _there is no ASP.NET command-line tool_ that generates new Controllers or Views. Determined not to use an <acronym title="Integrated Development Environment">IDE</acronym>, All of these blank files have to be filled in by hand (I have already done this for you):

```console
Songhay.ValidationWithMarkup.Web
├── Controllers
│   └── TodosController.cs
├── Views
│   ├── Todos
│   │   ├── Edit.cshtml
│   │   ├── EditorTemplates
│   │   │   └── TodoItem.cshtml
│   │   └── Index.cshtml
```

Note how the `Edit.cshtml` [file](../dotnet-web-mvc-unobtrusive-validation/Songhay.ValidationWithMarkup.Web/Views/Todos/Edit.cshtml) and the `TodoItem.cshtml` [file](../dotnet-web-mvc-unobtrusive-validation/Songhay.ValidationWithMarkup.Web/Views/Todos/EditorTemplates/TodoItem.cshtml) are heavily adorned with the conventional `data-` attributes required by unobtrusive validation.

### approach #2 (using `Html.EditorFor` and Data Annotations)

From the `dotnet-web-mvc-unobtrusive-validation/` [directory](../dotnet-web-mvc-unobtrusive-validation):

```bash
dotnet new mvc -o Songhay.ValidationWithAnnotations.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.ValidationWithAnnotations.Web/Songhay.ValidationWithAnnotations.Web.csproj

touch Songhay.ValidationWithAnnotations.Web/Controllers/TodosController.cs

touch Songhay.ValidationWithAnnotations.Web/Models/ITodosContext.cs
touch Songhay.ValidationWithAnnotations.Web/Models/TodoItem.cs
touch Songhay.ValidationWithAnnotations.Web/Models/TodoItem.cs
touch Songhay.ValidationWithAnnotations.Web/Models/TodosContext.cs

mkdir Songhay.ValidationWithAnnotations.Web/Views/Todos
mkdir Songhay.ValidationWithAnnotations.Web/Views/Todos/EditorTemplates
touch Songhay.ValidationWithAnnotations.Web/Views/Todos/EditorTemplates/TodoItem.cshtml
touch Songhay.ValidationWithAnnotations.Web/Views/Todos/Edit.cshtml
touch Songhay.ValidationWithAnnotations.Web/Views/Todos/Index.cshtml
```

Notice that, with this approach, we are copying _all_ of the `Songhay.Todo.Models` in order to add validation attributes. However, the `Edit.cshtml` [file](../dotnet-web-mvc-unobtrusive-validation/Songhay.ValidationWithMarkup.Web/Views/Todos/Edit.cshtml) and the `TodoItem.cshtml` [file](../dotnet-web-mvc-unobtrusive-validation/Songhay.ValidationWithMarkup.Web/Views/Todos/EditorTemplates/TodoItem.cshtml) have significantly less markup than the previous approach because validation declarations are no longer needed.

### approach #3 (repeating #2 with FluentValidation [[GitHub](https://github.com/FluentValidation/FluentValidation)] instead of Data Annotations)

```bash
dotnet new mvc -o Songhay.FluentValidation.Web

dotnet sln Songhay.UnobtrusiveValidation.sln \
      add Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj

dotnet add \
    Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj \
    reference ./Songhay.Todo/Songhay.Todo.csproj

dotnet add \
    Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj \
    package FluentValidation

dotnet add \
    Songhay.FluentValidation.Web/Songhay.FluentValidation.Web.csproj \
    package FluentValidation.AspNetCore

touch Songhay.FluentValidation.Web/Controllers/TodosController.cs

touch Songhay.FluentValidation.Web/Models/TodoItemValidator.cs
touch Songhay.FluentValidation.Web/Models/TodoListValidator.cs

mkdir Songhay.FluentValidation.Web/Views/Todos
touch Songhay.FluentValidation.Web/Views/Todos/EditorTemplates/TodoItem.cshtml
touch Songhay.FluentValidation.Web/Views/Todos/Edit.cshtml
touch Songhay.FluentValidation.Web/Views/Todos/Index.cshtml
```

This final (and preferred) approach can return to referencing the `Songhay.Todo/Models` [classes](../dotnet-web-mvc-unobtrusive-validation/Songhay.Todo/Models) like what is done in approach #1. Even the `TodoItemValidator` [class](../dotnet-web-mvc-unobtrusive-validation/Songhay.FluentValidation.Web/Models/TodoItemValidator.cs) and the `TodoListValidator` [class](../dotnet-web-mvc-unobtrusive-validation/Songhay.FluentValidation.Web/Models/TodoListValidator.cs) could be moved into the `Songhay.Todo/Models` namespace but are left where they are for the sake of self instruction.

## design details shared by all three approaches

All three approaches will use the same, famous models: a `TodoList` which contains a list of `TodoItem`. A readonly data source is represented by `TodosContext`, injected into the `TodosController` via `ITodosContext`.

### to benefit from unobtrusive validation, prefer `Html.EditorFor` over `Html.PartialAsync`

Because we are using a `TodoList` which contains a list of `TodoItem` we _must_ use `Html.EditorFor` which can be considered a perceived performance loss because we effectively _must_ load a potentially large Web page with many, many partials _synchronously_<sup>1</sup>, losing the benefits of `Html.PartialAsync`. This list of `TodoItem` is a _child_ collection that must be indexed in order to meet unobtrusive validation conventions. “[Collection Editing with MVC](https://www.abstractmethod.co.uk/blog/2017/12/collection-editing-with-mvc/)” details these conventions.

### `POST`ing the entire form for add/delete list item operations

The odious opinion here is that the use of “unobtrusive” validation means the design is avoiding the use of custom JavaScript completely. This means adding/deleting a `TodoItem` is achieved by `POST`ing the entire form which is very expensive and can introduce UX problems with browser history (hitting the back button in the browser). On the `TodosController` level, two methods are added, respectively:

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AddRow(TodoList data)
{
    var nextId = data.Items.Max(i => i.Id) + 1;
    data.Items.Add(new TodoItem { Id = nextId });
    return View(nameof(Edit), data);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult RemoveRow(TodoList data, int itemId)
{
    var index = data.Items.FindIndex(i => i.Id == itemId);
    data.Items.RemoveAt(index);
    return View(nameof(Edit), data);
}
```

The `TodoList data` arguments in both methods represent the entire form. ASP.NET Core conventions understand that `TodoList` needs to be `POST`ed back, simply because the command invoking `HttpPost` is a `button` element with attribute `type="submit"`.

However, the routes to these controller methods requires [Anchor Tag Helper](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-6.0) attributes:

```html
<button
    asp-controller="@nameof(TodosController).Replace(nameof(Controller), string.Empty)"
    asp-action="@nameof(TodosController.AddRow)"
    class="btn btn-primary" type="submit">Add Row</button>
```

and

```html
<button
    asp-controller="@nameof(TodosController).Replace(nameof(Controller), string.Empty)"
    asp-action="@nameof(TodosController.RemoveRow)"
    asp-route-itemId="@Model.Id"
    class="btn btn-outline-danger" title="Remove Row" type="submit">×</button>
```

, respectively.

In the `RemoveRow` button, notice that `asp-route-itemId` is being used, mapping to `int itemId` on the `RemoveRow` controller method. I used `itemId` instead of `id` to prevent confusing ASP.NET Core as it would not be sure that I intend to pass `TodoList.Id` or `TodoItem.Id` (it will choose the former).

## related links

- “[Setting up jQuery Unobtrusive Validation](https://www.mobzystems.com/blog/setting-up-jquery-unobtrusive-validation/)”

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

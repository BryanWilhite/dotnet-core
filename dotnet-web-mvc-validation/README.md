# ASP.NET MVC validation

This folder contains the various ways ASP.NET MVC supports validation.

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

@[BryanWilhite](https://twitter.com/BryanWilhite)

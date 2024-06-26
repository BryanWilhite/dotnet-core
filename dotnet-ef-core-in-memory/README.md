# Entity Framework Core with In-Memory Provider

This exercise can be regarded as a ‘minimalist’ installation of Entity Framework Core [📖 [docs](https://learn.microsoft.com/en-us/ef/core/)]. The goals here are:

- to define a repository, a `DbContext` [📖 [docs](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext)], with only one `DbSet<TEntity>` [📖 [docs](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1?view=efcore-8.0)]
- to define a single entity, `Blog`, that is a plain-old .NET class (no custom Entity Framework adornment)
- to test a theory about the repository: that it can insert and retrieve data in memory

## setup

```bash
dotnet new classlib -o InMemoryOne/InMemoryOne.Models
dotnet new classlib -o InMemoryOne/InMemoryOne.Repository
dotnet new xunit -o InMemoryOne/InMemoryOne.Tests

dotnet add \
    InMemoryOne/InMemoryOne.Repository/InMemoryOne.Repository.csproj \
    reference InMemoryOne/InMemoryOne.Models/InMemoryOne.Models.csproj
dotnet add \
    InMemoryOne/InMemoryOne.Tests/InMemoryOne.Tests.csproj \
    reference InMemoryOne/InMemoryOne.Models/InMemoryOne.Models.csproj
dotnet add \
    InMemoryOne/InMemoryOne.Tests/InMemoryOne.Tests.csproj \
    reference InMemoryOne/InMemoryOne.Repository/InMemoryOne.Repository.csproj

dotnet add \
    InMemoryOne/InMemoryOne.Repository/InMemoryOne.Repository.csproj \
    package Microsoft.EntityFrameworkCore.InMemory

dotnet new sln -n InMemoryOne -o InMemoryOne
dotnet sln InMemoryOne/InMemoryOne.sln add \
    InMemoryOne/InMemoryOne.Models/InMemoryOne.Models.csproj \
    InMemoryOne/InMemoryOne.Repository/InMemoryOne.Repository.csproj \
    InMemoryOne/InMemoryOne.Tests/InMemoryOne.Tests.csproj
```

We have added `Microsoft.EntityFrameworkCore.SqlServer` [[NuGet](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)] to [the test project](./InMemoryOne.Tests/BloggingContextTest.cs) in order to show the `DatabaseFacade.GetDbConnection()` [extension method](https://docs.microsoft.com/en-us/ef/core/api/microsoft.entityframeworkcore.relationaldatabasefacadeextensions) in action.

## related links

- “[Entity Framework Core Quick Overview](https://docs.microsoft.com/en-us/ef/core/)”
- “[Tutorial: Using Entity Framework Core as an In-Memory Database for ASP.NET Core](https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core)”
- [EF Core Testing Docs](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/)
- [GitHub Sample for EF Core Testing Docs](https://github.com/aspnet/EntityFramework.Docs/tree/master/samples/core/Miscellaneous/Testing)
- [Answer: “Entity Framework Core: How to get the Connection from the DbContext?”](https://stackoverflow.com/a/41936855/22944)
- “[Add code to initialize the database with test data](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro#add-code-to-initialize-the-database-with-test-data)”
- “[Microsoft.Data.Sqlite Namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite?view=msdata-sqlite-1.1.0)”
- “[SqliteConnection Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-1.1.0)”

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

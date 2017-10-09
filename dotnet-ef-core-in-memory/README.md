# Entity Framework Core with In-Memory Provider

```ps1
dotnet new classlib -o InMemoryOne.Models
dotnet new classlib -o InMemoryOne.Repository
dotnet new mstest -o InMemoryOne.Tests

dotnet add InMemoryOne.Repository/InMemoryOne.Repository.csproj reference InMemoryOne.Models/InMemoryOne.Models.csproj
dotnet add InMemoryOne.Tests/InMemoryOne.Tests.csproj reference InMemoryOne.Models/InMemoryOne.Models.csproj
dotnet add InMemoryOne.Tests/InMemoryOne.Tests.csproj reference InMemoryOne.Repository/InMemoryOne.Repository.csproj

dotnet add InMemoryOne.Repository/InMemoryOne.Repository.csproj package Microsoft.EntityFrameworkCore
dotnet add InMemoryOne.Repository/InMemoryOne.Repository.csproj package Microsoft.EntityFrameworkCore.InMemory
dotnet add InMemoryOne.Tests/InMemoryOne.Tests.csproj package Microsoft.EntityFrameworkCore
dotnet add InMemoryOne.Tests/InMemoryOne.Tests.csproj package Microsoft.EntityFrameworkCore.InMemory
```

## related links

* “[Entity Framework Core Quick Overview](https://docs.microsoft.com/en-us/ef/core/)”
* “[Tutorial: Using Entity Framework Core as an In-Memory Database for ASP.NET Core](https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core)”
* [EF Core Testing Docs](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/)
* [GitHub Sample for EF Core Testing Docs](https://github.com/aspnet/EntityFramework.Docs/tree/master/samples/core/Miscellaneous/Testing)
* “[Add code to initialize the database with test data](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro#add-code-to-initialize-the-database-with-test-data)”
* “[Microsoft.Data.Sqlite Namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite?view=msdata-sqlite-1.1.0)”
* “[SqliteConnection Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-1.1.0)”
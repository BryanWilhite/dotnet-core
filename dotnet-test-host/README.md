# integration testing ASP.NET Core with `Microsoft.AspNetCore.TestHost`

This sample demonstrates how integration testing works with `Microsoft.AspNetCore.TestHost` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.TestHost/)]. From the `dotnet-test-host` [folder](../dotnet-test-host), we add this package with these commands:

```ps1
dotnet new webapi -o Songhay.WebApiProject
dotnet new mstest -o Songhay.WebApiProject.Tests
dotnet add Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj reference Songhay.WebApiProject/Songhay.WebApiProject.csproj
dotnet add Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj package Microsoft.AspNetCore.TestHost
dotnet add Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj package Newtonsoft.Json
dotnet build Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj
```

## related links

* “[Integration testing in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing)”
* “[Integration Testing Your Asp Net Core App With An In Memory Database](http://www.stefanhendriks.com/2016/04/29/integration-testing-your-dot-net-core-app-with-an-in-memory-database/)”
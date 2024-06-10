# integration testing ASP.NET Core with `Microsoft.AspNetCore.Mvc.Testing`

This sample demonstrates how integration testing works with `Microsoft.AspNetCore.Mvc.Testing` [[nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing)]. From the `dotnet-web-mvc-testing` [directory](../dotnet-web-mvc-testing), we add this package with these commands:

```bash
dotnet new webapi -o Songhay.WebApiProject/Songhay.WebApiProject
dotnet new xunit -o Songhay.WebApiProject/Songhay.WebApiProject.Tests

dotnet add \
    Songhay.WebApiProject/Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj \
    reference Songhay.WebApiProject/Songhay.WebApiProject/Songhay.WebApiProject.csproj

dotnet add \
    Songhay.WebApiProject/Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj \
    package Microsoft.AspNetCore.Mvc.Testing

dotnet new sln -n Songhay.WebApiProject -o Songhay.WebApiProject

dotnet sln Songhay.WebApiProject/Songhay.WebApiProject.sln add \
      Songhay.WebApiProject/Songhay.WebApiProject/Songhay.WebApiProject.csproj \
      Songhay.WebApiProject/Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj

mv \
    Songhay.WebApiProject/Songhay.WebApiProject.Tests/UnitTest1.cs \
    Songhay.WebApiProject/Songhay.WebApiProject.Tests/WeatherForecastControllerTests.cs
```

Overwrite the `WeatherForecastControllerTests.cs` [file](./Songhay.WebApiProject/Songhay.WebApiProject.Tests/WeatherForecastControllerTests.cs) with the following tests:

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Songhay.WebApiProject.Tests
{
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<Songhay.WebApiProject.Startup>>
    {
        public WeatherForecastControllerTests(ITestOutputHelper helper, WebApplicationFactory<Songhay.WebApiProject.Startup> factory)
        {
            this._testOutputHelper = helper;
            this._factory = factory;
        }

        [Theory]
        [InlineData("/WeatherForecast")]
        public async Task Get_Test(string path)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            this._testOutputHelper.WriteLine($"GET: `{path}`");
            var response = await client.GetAsync(path);

            // Assert
            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadAsStringAsync();
            this._testOutputHelper.WriteLine($"{nameof(output)}:{Environment.NewLine}{output ?? "[null]"}");
            Assert.False(string.IsNullOrWhiteSpace(output));
        }

        readonly WebApplicationFactory<Songhay.WebApiProject.Startup> _factory;
        readonly ITestOutputHelper _testOutputHelper;
    }
}
```

```bash
dotnet build \
    Songhay.WebApiProject/Songhay.WebApiProject.sln

dotnet test \
    Songhay.WebApiProject/Songhay.WebApiProject.Tests/Songhay.WebApiProject.Tests.csproj \
    --logger:trx \
    --results-directory:./TestResults \
    --verbosity:normal
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

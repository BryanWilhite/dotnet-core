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
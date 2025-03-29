namespace Songhay.ContentNegotiation.Tests;

public class HostingEnvironmentTests : IClassFixture<WebApplicationFactory<Startup>>
{
    public HostingEnvironmentTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/", "Hello ASP.NET world!")]
    public async Task ShouldReturnExpectedResponseFromRoute(string route, string expectedResponse)
    {
        // arrange
        HttpClient client = _factory.CreateClient();

        // act
        HttpResponseMessage response = await client.GetAsync(route);
        response.EnsureSuccessStatusCode();

        // assert
        string actual = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedResponse, actual);
    }

    readonly WebApplicationFactory<Startup> _factory;
}

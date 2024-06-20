using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Songhay.ContentNegotiation.Tests.Controllers;

public class ContactsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    public ContactsControllerTests(ITestOutputHelper helper, WebApplicationFactory<Program> factory)
    {
        _testOutputHelper = helper;
        _factory = factory;
    }

    [Theory]
    [InlineData("api/contacts", "text/vcard", "Content-Type")]
    [InlineData("api/contacts", "application/json", "Content-Type")]
    public async Task Path_Contacts_Test(string route, string requestedType, string expectedResponseHeaderKey)
    {
        // arrange
        HttpClient client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Accept", requestedType);

        // act
        HttpResponseMessage response = await client.GetAsync(route);
        response.EnsureSuccessStatusCode();

        // assert
        string content = await response.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(content), "The expected content in the response is not here.");

        _testOutputHelper.WriteLine($"raw content: `{content}`");

        var hasExpectedHeader = response.Content.Headers
            .TryGetValues(expectedResponseHeaderKey, out IEnumerable<string> headerValues);

        Assert.True(hasExpectedHeader, "The expected header is not here.");
        Assert.True(headerValues.Count() == 1, "The expected number of header values is not here.");
        Assert.True(headerValues.First().StartsWith(requestedType), "The expected header value is not here.");
    }

    readonly ITestOutputHelper _testOutputHelper;
    readonly WebApplicationFactory<Program> _factory;
}
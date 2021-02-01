using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Songhay.ContentNegotiation.OutputFormatters;
using Songhay.Models;
using Xunit;
using Xunit.Abstractions;

namespace Songhay.ContentNegotiation.Tests.Controllers
{
    public class ContactsControllerTests
    {
        public ContactsControllerTests(ITestOutputHelper helper)
        {
            this.testOutputHelper = helper;

            var basePath = "../../../Songhay.ContentNegotiation";
            basePath = ProgramAssemblyUtility.GetPathFromAssembly(this.GetType().Assembly, basePath);

            builder = Program.CreateHostBuilder(args: null);
            Assert.NotNull(builder);

            builder
                .ConfigureWebHost(hostBuilder =>
                {
                    hostBuilder.UseTestServer();
                })
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    Assert.NotNull(builderContext);

                    var env = builderContext.HostingEnvironment;
                    Assert.NotNull(env);

                    env.ContentRootPath = $"{basePath}{Path.DirectorySeparatorChar}wwwroot";
                    env.EnvironmentName = "Development";
                });
        }

        [Theory]
        [InlineData("api/contacts", VcardOutputFormatter.MimeType, "Content-Type")]
        [InlineData("api/contacts", MimeTypes.ApplicationJson, "Content-Type")]
        public async Task Path_Contacts_Test(string path, string requestedType, string expectedResponseHeaderKey)
        {
            var host = await builder.StartAsync();

            var client = host.GetTestClient();

            client.DefaultRequestHeaders.Add("Accept", requestedType);

            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            Assert.False(string.IsNullOrWhiteSpace(content), "The expected content in the response is not here.");

            this.testOutputHelper.WriteLine($"raw content: `{content}`");

            var hasExpectedHeader = response.Content.Headers
                .TryGetValues(expectedResponseHeaderKey, out IEnumerable<string> headerValues);

            Assert.True(hasExpectedHeader, "The expected header is not here.");
            Assert.True(headerValues.Count() == 1, "The expected number of header values is not here.");
            Assert.True(headerValues.First().StartsWith(requestedType), "The expected header value is not here.");
        }

        readonly IHostBuilder builder;
        readonly ITestOutputHelper testOutputHelper;
    }
}
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Songhay.ContentNegotiation.OutputFormatters;
using Songhay.ContentNegotiation.Tests.Extensions;
using Songhay.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tavis.UriTemplates;

namespace Songhay.ContentNegotiation.Tests.Controllers
{
    [TestClass]
    public class ContactsControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            var basePath = this.TestContext.ShouldGetBasePath(this.GetType());
            var builder = Program.GetWebHostBuilder(args: null);
            Assert.IsNotNull(builder, "The expected Web Host builder is not here.");

            builder.ConfigureAppConfiguration((builderContext, configBuilder) =>
            {
                Assert.IsNotNull(builderContext, "The expected Web Host builder context is not here.");

                var env = builderContext.HostingEnvironment;
                Assert.IsNotNull(env, "The expected Hosting Environment is not here.");

                env.ContentRootPath = basePath;
                env.EnvironmentName = "Development";
                env.WebRootPath = $"{basePath}{Path.DirectorySeparatorChar}wwwroot";
            });

            this._server = new TestServer(builder);
            Assert.IsNotNull(this._server, "The expected test server is not here.");
        }

        [TestCategory("Integration")]
        [TestMethod]
        [TestProperty("pathTemplate", "api/contacts")]
        [TestProperty("expectedReponseHeaderKey", "Content-Type")]
        [TestProperty("requestedType", VcardOutputFormatter.MimeType)]
        public async Task ShouldGetContactsAsVcard()
        {
            #region test properties:

            var pathTemplate = new UriTemplate(this.TestContext.Properties["pathTemplate"].ToString());
            var expectedReponseHeaderKey = this.TestContext.Properties["expectedReponseHeaderKey"].ToString();
            var requestedType = this.TestContext.Properties["requestedType"].ToString();

            #endregion

            var path = pathTemplate.BindByPosition(null);
            var client = this._server.CreateClient();
            client.DefaultRequestHeaders.Add(HttpRequestHeader.Accept.ToString(), requestedType);
            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(content)) Assert.Fail("The expected content in the response is not here.");
            this.TestContext.WriteLine("raw content: {0}", content);

            var hasExpectedHeader = response.Content.Headers
                .TryGetValues(expectedReponseHeaderKey, out IEnumerable<string> headerValues);
            Assert.IsTrue(hasExpectedHeader, "The expected header is not here.");
            Assert.IsTrue(headerValues.Count() == 1, "The expected number of header values is not here.");
            Assert.IsTrue(headerValues.First().StartsWith(requestedType), "The expected header value is not here.");
        }

        [TestMethod]
        [TestProperty("pathTemplate", "api/contacts")]
        [TestProperty("expectedReponseHeaderKey", "Content-Type")]
        [TestProperty("expectedReponseHeaderValue", MimeTypes.ApplicationJson)]
        public async Task ShouldGetContactsByDefaultAsJson()
        {
            #region test properties:

            var pathTemplate = new UriTemplate(this.TestContext.Properties["pathTemplate"].ToString());
            var expectedReponseHeaderKey = this.TestContext.Properties["expectedReponseHeaderKey"].ToString();
            var expectedReponseHeaderValue = this.TestContext.Properties["expectedReponseHeaderValue"].ToString();

            #endregion

            var path = pathTemplate.BindByPosition(null);
            var client = this._server.CreateClient();
            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(content)) Assert.Fail("The expected content in the response is not here.");
            this.TestContext.WriteLine("raw content: {0}", content);

            var hasExpectedHeader = response.Content.Headers
                .TryGetValues(expectedReponseHeaderKey, out IEnumerable<string> headerValues);
            Assert.IsTrue(hasExpectedHeader, "The expected header is not here.");
            Assert.IsTrue(headerValues.Count() == 1, "The expected number of header values is not here.");
            Assert.IsTrue(headerValues.First().StartsWith(expectedReponseHeaderValue), "The expected header value is not here.");
        }

        TestServer _server;
    }
}

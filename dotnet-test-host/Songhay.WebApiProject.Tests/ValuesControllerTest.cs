using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Songhay.WebApiProject.Tests
{
    [TestClass]
    public class ValuesControllerTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            var builder = (new WebHostBuilder()).UseStartup<Startup>();
            this._server = new TestServer(builder);
        }

        [TestMethod]
        public void ShouldGenerateClient()
        {
            var client = this._server.CreateClient();
        }

        [TestMethod]
        [TestProperty("path", "api/values")]
        public async Task ShouldGetValuesAsync()
        {
            var path = this.TestContext.Properties["path"].ToString();
            var client = this._server.CreateClient();

            var response = await client.GetAsync(path);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsFalse(string.IsNullOrEmpty(responseString), "The expected response string is not here.");

            this.TestContext.WriteLine($"response: {responseString}");

            var jA = JArray.Parse(responseString);
            Assert.IsNotNull(jA, "The expected data is not here.");
            Assert.IsTrue(jA.Any(), "The expected data count is not here.");
        }

        TestServer _server;
    }
}
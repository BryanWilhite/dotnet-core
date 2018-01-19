using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Songhay.ContentNegotiation.Tests.Extensions;
using System.IO;

namespace Songhay.ContentNegotiation.Tests
{
    [TestClass]
    public class HostingEnvironmentTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ShouldGetWebHostBuilder()
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

            var server = new TestServer(builder);
            Assert.IsNotNull(server, "The expected test server is not here.");
        }
    }
}

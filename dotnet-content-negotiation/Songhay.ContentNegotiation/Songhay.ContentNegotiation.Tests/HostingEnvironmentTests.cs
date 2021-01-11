using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Songhay.ContentNegotiation.Tests
{
    public class HostingEnvironmentTests
    {
        [Theory]
        [InlineData("../../../Songhay.ContentNegotiation", "Hello World!")]
        public async Task CreateHostBuilder_Test(string basePath, string testResponse)
        {
            basePath = ProgramAssemblyUtility.GetPathFromAssembly(this.GetType().Assembly, basePath);

            var builder = Program.CreateHostBuilder(args: null);
            Assert.NotNull(builder);

            builder
                .ConfigureWebHost(hostBuilder =>
                {
                    hostBuilder.UseTestServer();
                    hostBuilder.Configure(appBuilder => appBuilder.Run(async ctx => await ctx.Response.WriteAsync(testResponse)));
                })
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    Assert.NotNull(builderContext);

                    var env = builderContext.HostingEnvironment;
                    Assert.NotNull(env);

                    env.ContentRootPath = $"{basePath}{Path.DirectorySeparatorChar}wwwroot";
                    env.EnvironmentName = "Development";
                });

            var host = await builder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(testResponse, responseString);
        }
    }
}
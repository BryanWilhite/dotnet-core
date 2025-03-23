using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MyFunctions;

public static class Program
{
    public static IHost HostInstance { get; private set; }

    public static async Task Main(string[] args)
    {
        HostApplicationBuilder builder =  Host.CreateApplicationBuilder(args);

        HostInstance = await Task.FromResult(builder.Build());
    }
}
using Songhay.Diagnostics;
using Songhay.Extensions;
using Songhay.Models;
using System.Diagnostics;
using System.IO;

namespace Songhay.ListenerTwo.Shell
{
    class Program
    {
        static Program()
        {
            var configuration = ProgramUtility.LoadConfiguration(Directory.GetCurrentDirectory());

            TraceSources.ConfiguredTraceSourceName = configuration[DeploymentEnvironment.DefaultTraceSourceNameConfigurationKey];

            traceSource = TraceSources
                .Instance
                .GetTraceSourceFromConfiguredName()
                .WithSourceLevels();
        }

        static readonly TraceSource traceSource;

        static void Main(string[] args)
        {
            var activity = new BusinessOneActivity();
            activity.StartConsoleActivity(new ProgramArgs(args), traceSource);
        }
    }
}

using Songhay.Diagnostics;
using Songhay.Extensions;
using Songhay.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace Songhay.ListenerOne.Shell
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
            using (var listener = new TextWriterTraceListener(Console.Out))
            {
                traceSource?.Listeners.Add(listener);

                try
                {
                    var biz = new BusinessOne();
                    biz.DoBusiness();
                }

                finally { listener.Flush(); }
            }
        }
    }
}
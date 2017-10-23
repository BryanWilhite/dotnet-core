using Songhay.Diagnostics;
using Songhay.Extensions;
using System.Diagnostics;

namespace Songhay.ListenerOne.Shell
{
    public class BusinessOne
    {
        static BusinessOne()
        {
            TraceSourceName = "rx-trace";
            traceSource = TraceSources
                .Instance[BusinessOne.TraceSourceName]
                .WithAllSourceLevels();
        }

        public void DoBusiness()
        {
            traceSource.TraceInformation("Hello business!");
        }

        internal static string TraceSourceName { get; private set; }

        static readonly TraceSource traceSource;
    }
}
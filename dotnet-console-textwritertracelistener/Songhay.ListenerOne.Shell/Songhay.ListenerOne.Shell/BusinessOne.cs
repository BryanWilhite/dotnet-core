using Songhay.Diagnostics;
using Songhay.Extensions;
using System.Diagnostics;

namespace Songhay.ListenerOne.Shell
{
    public class BusinessOne
    {
        static BusinessOne() => traceSource = TraceSources
            .Instance
            .GetTraceSourceFromConfiguredName()
            .WithSourceLevels();

        static readonly TraceSource traceSource;

        public void DoBusiness()
        {
            traceSource?.TraceInformation("Hello business!");
        }
    }
}
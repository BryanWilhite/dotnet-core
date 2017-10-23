using System.Diagnostics;

namespace Songhay.ListenerOne.Shell
{
    public class BusinessOne
    {
        public void DoBusiness(TraceListener listener)
        {
            //var listener = Trace.Listeners[0];
            var traceSource = new TraceSource("rx", SourceLevels.All);
            traceSource.Listeners.Add(listener);
            traceSource.TraceInformation("Hello business!");
        }
    }
}
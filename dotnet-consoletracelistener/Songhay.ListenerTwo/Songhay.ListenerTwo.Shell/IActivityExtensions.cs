using System.Diagnostics;
using Songhay.Models;

namespace Songhay.ListenerTwo
{
    public static class IActivityExtensions
    {
        public static void StartConsoleActivity(this IActivity activity, ProgramArgs args, TraceSource traceSource)
        {
            using (var listener = new ConsoleTraceListener())
            {
                traceSource?.Listeners.Add(listener);

                try
                {
                    activity.Start(args);
                }
                finally
                {
                    listener.Flush();
                }
            }
        }
    }
}
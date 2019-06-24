using Songhay.Diagnostics;
using Songhay.Extensions;
using System;
using System.Diagnostics;

namespace Songhay.ListenerOne.Shell
{
    class Program
    {
        static Program() => traceSource = TraceSources
            .Instance
            .GetTraceSourceFromConfiguredName()
            .WithSourceLevels();

        static readonly TraceSource traceSource;

        static void Main(string[] args)
        {
            using(var listener = new TextWriterTraceListener(Console.Out))
            {
                traceSource?.Listeners.Add(listener);

                var biz = new BusinessOne();
                biz.DoBusiness();

                listener.Flush();
            }
        }
    }
}
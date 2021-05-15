using Songhay.Diagnostics;
using Songhay.Extensions;
using Songhay.Models;
using System.Diagnostics;

namespace Songhay.ListenerTwo
{
    public class BusinessOneActivity : IActivity
    {
        static BusinessOneActivity() => traceSource = TraceSources
            .Instance
            .GetTraceSourceFromConfiguredName()
            .WithSourceLevels();

        static readonly TraceSource traceSource;

        public string DisplayHelp(ProgramArgs args) => "There is no help.";

        public void Start(ProgramArgs args) =>
            traceSource?.WriteLine(this.DoMyOtherRoutine(this.DoMyRoutine()));

        internal string DoMyOtherRoutine(string input) =>
            $"The other routine is done. [{nameof(input)}: {input ?? "[null]"}]";

        internal string DoMyRoutine() => "The routine is done.";
    }
}
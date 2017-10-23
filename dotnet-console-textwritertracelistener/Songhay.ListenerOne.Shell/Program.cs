using System;
using System.Diagnostics;

namespace Songhay.ListenerOne.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var listener = new TextWriterTraceListener(Console.Out))
            {
                var biz = new BusinessOne();
                biz.DoBusiness(listener);

                listener.Flush();
            }
        }
    }
}
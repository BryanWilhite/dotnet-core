# .NET Core `TextWriterTraceListener`

This sample demonstrates the only way (as of this writing) to use `TraceSource` with Console output in .NET Core:

```c#
using(var listener = new TextWriterTraceListener(Console.Out))
{
    traceSource.Listeners.Add(listener);

    var biz = new BusinessOne();
    biz.DoBusiness();

    listener.Flush();
}
```

This current default behavior in .NET Core places `TextWriterTraceListener` front and center for any `TraceSource` related diagnostics on .NET Core.

## Why a `TraceSource` logging pattern?

The `TraceSource` logging pattern is a slightly verbose alternative to the `ILogger`/`ServiceCollection` logging pattern. The main reason why I would use a `TraceSource` logging pattern is because `TraceSource` is the most flexible of logging alternatives. It is completely decoupled from everything except the .NET Framework itself—and, in .NET Core, Microsoft provides us with `Microsoft.Extensions.Logging.TraceSource` [[NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Logging.TraceSource)]. Using `TraceSource` allows me to pass around code in a large enterprise without demanding that the recipient should use a particular logging framework. For this reason alone it _should_ be supported in .NET Core.

The one issue one might have with `TraceSource` is the fact that the `Trace` [class](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.trace?view=netstandard-2.0) keeps track of listeners but not trace sources. Because of this, I had to introduce a dependency from my `SonghayCore` project, the `TraceSources` [class](https://github.com/BryanWilhite/SonghayCore/blob/master/SonghayCore/Diagnostics/TraceSources.cs). I would prefer that the functionality in `TraceSources` be in Microsoft’s `Trace` class.

Another issue one might have with this approach is the need to declare a static `TraceSource` in every single class concerned with tracing. In this code sample, we see the use of these static members in the static constructors of the `Program` [class](./Songhay.ListenerOne.Shell/Songhay.ListenerOne.Shell/Program.cs) and the `BusinessOne` [class](./Songhay.ListenerOne.Shell/Songhay.ListenerOne.Shell/BusinessOne.cs). One might call this the “`TraceSource` tax” (which is similar to throwing `ILogger` statements throughout our code).

We also should see that `WithAllSourceLevels()` is an Extension Method from my `SonghayCore` project, in the `TraceSourceExtensions` [class](https://github.com/BryanWilhite/SonghayCore/blob/master/SonghayCore/Extensions/TraceSourceExtensions.cs). This implies yet more dependency but also shows us that we have the ability to instantiate and filter `TraceSource` per class. However, by default, we see that `TraceSourceName` in the `BusinessOne` [class](./Songhay.ListenerOne.Shell/BusinessOne.cs) of this sample sets one `TraceSource` name which reveals the intent to share _one_ instance of `TraceSource` for the whole application. Again, we have the freedom to change this per class—one of the benefits of paying the `TraceSource` tax.

Finally, we must point out that `TraceSourceName` can be loaded from [configuration](https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/).

* [Using Dependency Injection in .NET Core Console Apps](http://asp.net-hacker.rocks/2017/02/08/using-dependency-injection-in-dotnet-core-console-apps.html)
* [TextWriterTraceListener Class](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.textwritertracelistener?view=netframework-4.7.1)
* [A Tracing Primer – Part I [Mike Rousos]](https://blogs.msdn.microsoft.com/bclteam/2005/03/15/a-tracing-primer-part-i-mike-rousos/)
* [Using strongly typed configuration in .NET Core console app](https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/)

@[BryanWilhite](https://twitter.com/bryanwilhite)

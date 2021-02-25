# .NET cross-platform exploration

I enjoyed many years using [LINQPad](https://www.linqpad.net/) on Windows to play around and experiment with .NET Framework. My [LinqPad repo](https://github.com/BryanWilhite/linqPad/) represents _years_ of that effort.

Now I need to move forward with .NET 5 and later. I could stay on Windows with [Joseph Albahari](http://www.albahari.com/) eventually working past [LINQPad 6](https://www.linqpad.net/LINQPad6.aspx) but I feel like I need to go forward with a cross platform approach—and Joseph should agree that there is not one UI-based way to go in this direction.

We make the first move from the `dotnet-tests` [folder](../dotnet-tests):

```bash
dotnet new xunit -o Songhay.DotNet/Songhay.DotNet.Tests
```

@[BryanWilhite](https://twitter.com/BryanWilhite)

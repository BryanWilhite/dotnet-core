# .NET cross-platform exploration

I enjoyed many years using [LINQPad](https://www.linqpad.net/) on Windows to play around and experiment with .NET Framework. My [LinqPad repo](https://github.com/BryanWilhite/linqPad/) represents _years_ of that effort.

Now I need to move forward with .NET 5 and later. I could stay on Windows with [Joseph Albahari](http://www.albahari.com/) eventually working past [LINQPad 6](https://www.linqpad.net/LINQPad6.aspx) but I feel like I need to go forward with a cross platform approach—and Joseph should agree that there is not one UI-based way to go in this direction.

We make the first moves from the `dotnet-tests` [folder](../dotnet-tests):

```bash
dotnet new sln -n Songhay.DotNet -o Songhay.DotNet
dotnet new xunit -o Songhay.DotNet/Songhay.DotNet/Songhay.DotNet.Tests
dotnet sln Songhay.DotNet/Songhay.DotNet.sln add \
    Songhay.DotNet/Songhay.DotNet/Songhay.DotNet.Tests/Songhay.DotNet.Tests.csproj
```

@[BryanWilhite](https://twitter.com/BryanWilhite)

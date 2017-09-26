# CSI.EXE minimal

This sample represents an exploration of [CSI.EXE](https://msdn.microsoft.com/en-us/magazine/mt614271.aspx), C-Sharp Interactive over Roslyn. [Filip W.](https://github.com/filipw) worked on the [scriptcs](https://github.com/scriptcs/scriptcs) project and [he expresses a transition](https://www.strathweb.com/2016/12/writing-c-build-scripts-with-fake-omnisharp-and-vs-code/) similar to mine: a move away from scriptcs toward a more _direct_ relationship with Roslyn.

**Very important:** CSI.EXE is not a part of .NET Core and it has no official, Microsoft-released cross-platform story. There are [Roslyn Binaries in Mono](https://github.com/mono/roslyn-binaries) that deserve further investigation (for those who are willing to mix Mono and .NET Core together). The assumption here is that Microsoft will release a cross-platform Roslyn-based scripting story that works from the [dotnet CLI](https://github.com/dotnet/cli) and [#4672 on their “wish list”](https://github.com/dotnet/cli/issues/4672) will address this. In the mean time Microsoft gives us [Microsoft.CodeAnalysis.CSharp.Scripting](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Scripting/#dotnet-cli) for .NET Core and the full .NET Framework—_all_ scripting solutions for .NET should be using this package.

That _direct_ relationship on Windows is in the form of C-Sharp Interactive, CSI.EXE. This sample will show that CSI.EXE (which is available in [Build Tools for Visual Studio 2017](https://www.visualstudio.com/downloads/#build-tools-for-visual-studio-2017)) _can_ work with NuGet packages (via `#r` directives that do not support NuGet packages as of this writing).

Simultaneously, [Filip W. is working](https://www.strathweb.com/2016/10/introducing-c-script-runner-for-net-core-and-net-cli/) on a cross-platform `*.csx` scripting solution on .NET core around a `dotnet script` command. His project uses `#r` directives that do support NuGet (e.g. `#r "nuget:NetStandard.Library,1.6.1"`). I think this is the future of `*.csx` scripting (see on [GitHub](https://github.com/filipw/dotnet-script)) but among Windows-only alternatives, CSI.EXE will remain the conservative fallback. I have taken a look at his work in the [`dotnet-script-minimal`](../dotnet-script-minimal) folder of this repository.

This exploration will take place in [Visual Studio Code](https://code.visualstudio.com/) with no directly-relating extensions installed (apart from [ms-vscode.csharp](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)).

## Environment (without Visual Studio 2017)

An environment without Visual Studio 2017 (like a build server) requires installing [Build Tools for Visual Studio 2017](https://www.visualstudio.com/downloads/#build-tools-for-visual-studio-2017) and `Nuget.exe`.

`NuGet.exe` is installed with [Download-NuGet.ps1](./ps1/Download-NuGet.ps1). These commands (from [this folder](../csi-exe-minimal)) set up the environment:

```ps1
.\ps1\Download-NuGet.ps1
.\ps1\AddToEnvironmentVariable-Path.ps1
```

## Environment (with Visual Studio 2017)

`NuGet.exe` is installed with [Download-NuGet.ps1](./ps1/Download-NuGet.ps1). These commands (from [this folder](../csi-exe-minimal)) set up the environment:

```ps1
.\ps1\Download-NuGetForVS.ps1
.\ps1\AddToEnvironmentVariable-PathForVS.ps1
```
# CSI.EXE minimal

This sample represents an exploration of CSI.EXE, C-Sharp Interactive over Roslyn. Filip W. worked on the [scriptcs](https://github.com/scriptcs/scriptcs) project and [he expresses a transition](https://www.strathweb.com/2016/12/writing-c-build-scripts-with-fake-omnisharp-and-vs-code/) similar to mine: a move away from scriptcs toward a more direct relationship with Roslyn.

**Very important:** CSI.EXE is not a part of .NET Core. This serves here to illustrate one reason why one would move to .NET Core.

That direct relationship on Windows is in the form of C-Sharp Interactive, [CSI.EXE](https://msdn.microsoft.com/en-us/magazine/mt614271.aspx). The work in this repository will show that CSI.EXE (which is available in [Build Tools for Visual Studio 2017](https://www.visualstudio.com/downloads/#build-tools-for-visual-studio-2017)) _can_ work with NuGet packages (via `#r` directives that do not support NuGet packages as of this writing).

Simultaneously, [Filip W. is working](https://www.strathweb.com/2016/10/introducing-c-script-runner-for-net-core-and-net-cli/) on a cross-platform `*.csx` scripting solution on .NET core around a `dotnet script` command. His project uses `#r` directives that do support NuGet (e.g. `#r "nuget:NetStandard.Library,1.6.1"`). I think this is the future of `*.csx` scripting (see on [GitHub](https://github.com/filipw/dotnet-script)) but among Windows-only alternatives, CSI.EXE will remain the conservative fallback.

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
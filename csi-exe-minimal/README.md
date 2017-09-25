# CSI.EXE minimal

This sample represents an exploration of CSI.EXE, C-Sharp Interactive over Roslyn. Filip W. worked on the [scriptcs](https://github.com/scriptcs/scriptcs) project and [he expresses a transition](https://www.strathweb.com/2016/12/writing-c-build-scripts-with-fake-omnisharp-and-vs-code/) similar to mine: a move away from scriptcs toward a more direct relationship with Roslyn.

That direct relationship on Windows is in the form of C-Sharp Interactive, [CSI.EXE](https://msdn.microsoft.com/en-us/magazine/mt614271.aspx). The work in this repository will show that CSI.EXE (which is available in [Build Tools for Visual Studio 2017](https://www.visualstudio.com/downloads/#build-tools-for-visual-studio-2017)) _can_ work with NuGet packages.

The plan is to use NuGet directly and write a `*.csx` script that generates another `*.csx` script full of `#r` directives based on `package.config`. Accomplishing this will effectively eliminate _my_ need for scriptcs (because the scriptcs dependency on [Mono](http://www.mono-project.com/) on Linux is not appealing to me). However the story on Linux for CSI.EXE is still a mystery to me as it is a product in the Visual Studio family (I assume scriptcs is using [xbuild in Mono](http://www.mono-project.com/docs/tools+libraries/tools/xbuild/)).

Simultaneously, [Filip W. is working](https://www.strathweb.com/2016/10/introducing-c-script-runner-for-net-core-and-net-cli/) on a cross-platform `*.csx` scripting solution on .NET core around a `dotnet script` command. I think this is the future of `*.csx` scripting (see on [GitHub](https://github.com/filipw/dotnet-script)) but for Windows-only alternatives to PowerShell CSI.EXE will remain the conservative fallback.

## Environment

This exploration will take place in [Visual Studio Code](https://code.visualstudio.com/) with no directly-relating extensions installed (apart from [ms-vscode.csharp](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)). In addition to installing [Build Tools for Visual Studio 2017](https://www.visualstudio.com/downloads/#build-tools-for-visual-studio-2017)), `NuGet.exe` is installed with [Download-NuGet.ps1](./ps1/Download-NuGet.ps1).

Of course `"${env:ProgramFiles(x86)}\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\Roslyn"` is added to `$env:Path`.
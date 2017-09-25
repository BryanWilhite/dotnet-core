$outFolder = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\Roslyn"

Invoke-WebRequest `
    -OutFile "$outFolder\nuget.exe" `
    -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

Get-ChildItem -Path $outFolder -File nuget.exe
# dotnet script minimal

This is the minimal required to get dotnet script ([GitHub](https://github.com/filipw/dotnet-script)) working, following the repository documentation. My only twist on this is my preference to standardize on Ubuntu bash, across platforms. However, Powershell setup notes are below…

From the root folder of this sample this command should run without errors:

```bash
dotnet script hello-world.csx
```

## setup on Ubuntu bash

dotnet script currently requires .NET Core 1.1.2 which is in SDK version 1.0.4:

```bash
sudo apt-get install dotnet-dev-1.0.4
```

A path must set to the dotnet script folder (in `~/.profile`):

```bash
DOTNET_SCRIPT=/your/path/to/dotnet-script.0.13.0/dotnet-script
export PATH=$PATH:$DOTNET_SCRIPT/
```

In the [Windows Subsystem for Linux](https://msdn.microsoft.com/en-us/commandline/wsl/about), edit with, say, [pico](https://en.wikipedia.org/wiki/Pico_(text_editor)) by opening `.profile` a new terminal session. By default, your present working directory should be your `/home/*/` folder and this command should work:

```bash
pico .profile
source ~/.profile
```

You may have to run source `~/.profile` with every new terminal session until you log out and log back in.

## setup on PowerShell

The dotnet script README file gives us this single [Chocolatey](https://chocolatey.org/) setup command:

```ps1
choco install dotnet.script
```

I recommend using [Chocolatey GUI](https://chocolatey.org/packages/ChocolateyGUI) to  install the [dotnet.script Chocolatey package](https://chocolatey.org/packages/dotnet.script). As of this writing, [.NET Core SDK 1.0.4](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md) is required to run dotnet script.

_Visual Studio Code users_: install the [PowerShell](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell) extension to get the **PowerShell: Show Integrated Console** command to run PowerShell and bash sessions on top of each other in Visual Studio Code.

Add the dotnet script path to Windows (in Powershell) with [AddToEnvironmentVariable-Path.ps1](./ps1/AddToEnvironmentVariable-Path.ps1).

## related resources

* “[.NET Core 1.0.5 & 1.1.2 SDK 1.0.4](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md)”
* “[Permanent PATH variable](https://askubuntu.com/questions/500775/permanent-path-variable)”
* “[Switch between dotnet core SDK versions](https://stackoverflow.com/questions/42077229/switch-between-dotnet-core-sdk-versions)”
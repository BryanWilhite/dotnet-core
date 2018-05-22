# dotnet script minimal

This is the minimal required to get `dotnet-script` [[GitHub](https://github.com/filipw/dotnet-script)] working, following the repository documentation. My only twist on this is my preference to standardize on Ubuntu bash, across platforms. However, Powershell setup notes are below…

From the root folder of this sample this command should run without errors:

```bash
sdotnet script hello-world.csx
```

## setup on Ubuntu bash

See “[Install .NET Core SDK on Linux Ubuntu 16.04](https://www.microsoft.com/net/download/linux-package-manager/ubuntu16-04/sdk-current)” and instead of installing `dotnet-sdk-2.1.200` install `dotnet-sdk-2.1.300-rc1-008673`:

```bash
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-2.1.300-rc1-008673
```

Then, install it as a _global tool_ (introduced with .NET Core 2.1):

```bash
dotnet tool install -g dotnet-script
```

## setup on PowerShell

The dotnet script README file gives us this single [Chocolatey](https://chocolatey.org/) setup command:

```ps1
choco install dotnet.script
```

I recommend using [Chocolatey GUI](https://chocolatey.org/packages/ChocolateyGUI) to  install the [dotnet.script Chocolatey package](https://chocolatey.org/packages/dotnet.script). As of this writing, [.NET Core SDK 1.0.4](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md) is required to run dotnet script.

Add the dotnet script path to Windows (in Powershell) with [AddToEnvironmentVariable-Path.ps1](./ps1/AddToEnvironmentVariable-Path.ps1).

## related resources

* “[.NET Core 1.0.5 & 1.1.2 SDK 1.0.4](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md)”
* “[Permanent PATH variable](https://askubuntu.com/questions/500775/permanent-path-variable)”
* “[Switch between dotnet core SDK versions](https://stackoverflow.com/questions/42077229/switch-between-dotnet-core-sdk-versions)”
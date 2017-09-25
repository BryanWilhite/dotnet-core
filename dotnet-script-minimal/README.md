# dotnet script minimal

This is the minimal required to get dotnet script ([GitHub](https://github.com/filipw/dotnet-script)) working, following the repository documentation. My only twist on this is my preference to standardize on Ubuntu bash, across platforms.

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

In the [Windows Subsystem for Linux](https://msdn.microsoft.com/en-us/commandline/wsl/about), edit with, say, pico by opening `.profile` a new terminal session. By default, your present working directory should be your `/home/*/` folder and this command should work:

```bash
pico .profile
source .profile
```

## related resources

* “[.NET Core 1.0.5 & 1.1.2 SDK 1.0.4](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.2-download.md)”
* “[Permanent PATH variable](https://askubuntu.com/questions/500775/permanent-path-variable)”
* “[Switch between dotnet core SDK versions](https://stackoverflow.com/questions/42077229/switch-between-dotnet-core-sdk-versions)”
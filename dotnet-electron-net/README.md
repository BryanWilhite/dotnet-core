# Electron.NET

>Electron.NET is a wrapper around a native Electron application with an embedded ASP.NET Core application. Via our Electron.NET IPC bridge we can invoke Electron APIs from .NET.
>
>The CLI extensions hosts our toolset to build and start Electron.NET applications.
>
>—<https://github.com/ElectronNET/Electron.NET>
>

To install the CLI extensions, run:

```bash
dotnet tool install ElectronNET.CLI -g
```

From the `/dotnet-electron-net` [directory](../dotnet-electron-net), run:

```bash
dotnet new webapp -n MyWebApp
```

From the `/dotnet-electron-net/MyWebApp` [directory](../dotnet-electron-net/MyWebApp), run:

```bash
electronize init
```

The output of this `init` command should be similar to:

```console
Adding our config file to your project...
Search your .csproj/fsproj to add the needed electron.manifest.json...
Found your .csproj: /dotnet-core/dotnet-electron-net/MyWebApp/MyWebApp.csproj - check for existing config or update it.
electron.manifest.json will be added to csproj/fsproj.
electron.manifest.json added in csproj/fsproj!
Search your .launchSettings to add our electron debug profile...
Debug profile added!
Everything done - happy electronizing!
```

From the `/dotnet-electron-net/MyWebApp` [directory](../dotnet-electron-net/MyWebApp), run:

```console
electronize start
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

# CSI.EXE with NuGet.EXE

In this most primal example, `main.csx` references the `net45` version of `NewtonSoft.Json`. From the `.\example01` folder, this NuGet package is installed with:

```powershell

NuGet install NewtonSoft.Json -OutputDirectory .\packages

```

Review the `#r` directive such that the folder/version-number suffixes match. So, for `\example01\packages\Newtonsoft.Json.11.0.2` the Newtonsoft directive in the `main.csx` [script](./main.csx#L1) must read:

```c#
#r ".\packages\Newtonsoft.Json.11.0.2\lib\net45\Newtonsoft.Json.dll"
```

We then run with CSI.EXE:

```powershell

csi .\main.csx

```
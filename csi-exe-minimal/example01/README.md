# CSI.EXE with NuGet.EXE

In this most primal example, `main.csx` references the `net45` version of `NewtonSoft.Json`. From the `.\example01` folder, this NuGet package is installed with:

```powershell

NuGet install NewtonSoft.Json -OutputDirectory .\packages

```

We then run with CSI.EXE:

```powershell

csi .\main.csx

```
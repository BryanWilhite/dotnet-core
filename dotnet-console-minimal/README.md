# .NET Core console app, minimal

## hwapp

Microsoft introduces .NET Core to us through its `hwapp` walk-through (which is the Console Application template you can see in the output of `dotnet new`). I went over the [Linux Ubuntu path](https://www.microsoft.com/net/core#linuxubuntu) using the Linux Subsystem for Windows.

I have also included the F# version of the `hwapp` in the [`hwapp-fsharp`](./hwapp-fsharp) folder with this command (from the parent folder):

```bash
dotnet new console -o hwapp-fsharp -lang F#
```

For more detail, see [dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore2x).
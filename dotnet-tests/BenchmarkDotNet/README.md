# `BenchmarkDotNet`

>BenchmarkDotNet helps you to transform methods into benchmarks, track their performance, and share reproducible measurement experiments. It's no harder than writing unit tests!
>
><https://benchmarkdotnet.org/>

## setting up a `BenchmarkDotNet`

Let’s generate a `*.Shell` console-app solution for our curated collection of bench marks. From the `BenchmarkDotNet/` [directory](../BenchmarkDotNet):

```bash
dotnet new sln -n BenchmarkDotNet
dotnet new console -n BenchmarkDotNet.Shell
dotnet sln BenchmarkDotNet.sln add \
    BenchmarkDotNet.Shell/BenchmarkDotNet.Shell.csproj

dotnet add \
    BenchmarkDotNet.Shell/BenchmarkDotNet.Shell.csproj \
    package BenchmarkDotNet
```

I prefer to not use `BenchmarkDotNet` attributes decorating classes under test which leads me to add fluent specifications in the `Program.cs` [file](./BenchmarkDotNet.Shell/Program.cs):

```csharp
static void Main(string[] args)
{
    var job = Job.Default
        .WithPlatform(Platform.X64)
        .WithNoAffinitize(true)
        .WithStrategy(RunStrategy.Throughput)
        .WithUnrollFactor(16)
        ;

    var exporters = new[]
    {
        MarkdownExporter.GitHub
    };

    var loggers = new[]
    {
        ConsoleLogger.Unicode
    };

    var config = ManualConfig
        .CreateEmpty()
        .AddColumnProvider(DefaultColumnProviders.Descriptor)
        .AddColumnProvider(DefaultColumnProviders.Metrics)
        .AddColumnProvider(DefaultColumnProviders.Params)
        .AddColumnProvider(DefaultColumnProviders.Statistics)
        .AddExporter(exporters)
        .AddLogger(loggers)
        .AddJob(job)
        .WithOption(ConfigOptions.DisableLogFile, false)
        .WithOption(ConfigOptions.DontOverwriteResults, false)
        .WithOption(ConfigOptions.KeepBenchmarkFiles, false)
        .WithOption(ConfigOptions.StopOnFirstError, false)
        ;

    BenchmarkRunner.Run<MyBenchmarks.Md5VsSha256>(config);
}
```

Note that `MarkdownExporter.GitHub` is specified, generating a `Md5VsSha256-report-github.md` [file](./BenchmarkDotNet.Shell/BenchmarkDotNet.Artifacts/results/MyBenchmarks.Md5VsSha256-report-github.md) in the conventional `results/` directory.

## `Permission denied` error message on linux

Not only does `BenchmarkDotNet` require a release build for testing, on Linux it needs `sudo` permissions to avoid this error:

```plaintext
Failed to set up high priority. Make sure you have the right permissions. Message: Permission denied
```

To avoid this error, we run from the [directory](./BenchmarkDotNet.Shell):

```bash
sudo dotnet run --configuration release
```

## getting started with `BenchmarkDotNet`

I am going to jog real quick through 👟 the “[Getting Started](https://benchmarkdotnet.org/articles/guides/getting-started.html)” guide by manually adding `MyBenchmarks/Md5VsSha256.cs` to the `BenchmarkDotNet.Shell` [directory](./BenchmarkDotNet.Shell).

@[BryanWilhite](https://twitter.com/BryanWilhite)

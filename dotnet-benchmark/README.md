# `BenchmarkDotNet`

>BenchmarkDotNet helps you to transform methods into benchmarks, track their performance, and share reproducible measurement experiments. It's no harder than writing unit tests!
>
><https://benchmarkdotnet.org/>

## setting up a `BenchmarkDotNet`

Let’s generate a `*.Shell` console-app solution for our curated collection of bench marks. From the `dotnet-benchmark/` [directory](../dotnet-benchmark):

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
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

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
```

Note that `MarkdownExporter.GitHub` is specified, generating a `Md5VsSha256-report-github.md` [file](./BenchmarkDotNet.Shell/BenchmarkDotNet.Artifacts/results/MyBenchmarks.Md5VsSha256-report-github.md) in the conventional `dotnet-benchmark/BenchmarkDotNet.Shell/BenchmarkDotNet.Artifacts/results/` [directory](../dotnet-benchmark/BenchmarkDotNet.Shell/BenchmarkDotNet.Artifacts/results).

## getting started with `BenchmarkDotNet`

I am going to jog real quick through 👟 the “[Getting Started](https://benchmarkdotnet.org/articles/guides/getting-started.html)” guide by manually adding the `MyBenchmarks/Md5VsSha256.cs` file to the `dotnet-benchmark/BenchmarkDotNet.Shell` [directory](../dotnet-benchmark/BenchmarkDotNet.Shell). Then, from the `dotnet-benchmark/` [directory](../dotnet-benchmark), run:

```bash
dotnet run \
    --project BenchmarkDotNet.Shell/BenchmarkDotNet.Shell.csproj \
    --configuration release
```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼

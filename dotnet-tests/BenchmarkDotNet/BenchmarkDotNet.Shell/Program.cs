using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace BenchmarkDotNet.Shell
{
    class Program
    {
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

            BenchmarkRunner.Run<MyBenchmarks.JObjectVsJsonDocument>(config);
        }
    }
}

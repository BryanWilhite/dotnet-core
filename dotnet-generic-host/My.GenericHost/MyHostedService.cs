using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace My.Hosting;

/// <summary>
/// Defines the conventional, default Hosted Service
/// </summary>
public class MyHostedService: IHostedService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultHostedService"/> class.
    /// </summary>
    /// <param name="hostApplicationLifetime">the <see cref="IHostApplicationLifetime"/></param>
    /// <param name="logger">the <see cref="ILogger"/></param>
    public MyHostedService(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime, ILogger<MyHostedService> logger)
    {
        _configuration = configuration;
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
    }

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(() =>
        {
            _logger.LogInformation("Displaying configuration...");

            IReadOnlyCollection<string> keys = _configuration
                .AsEnumerable().Select(kv => kv.Key).ToArray();

            StringBuilder sb = new();

            foreach (string key in keys)
            {
                sb.AppendFormat("    {0}: {1}{2}", key, _configuration[key], Environment.NewLine);
            }

            _logger?.LogInformation("{Lines}", sb.ToString());

            //simple key-value pair:
            _logger?.LogInformation("defaultTraceSourceName: {Name}",
                _configuration["defaultTraceSourceName"]);

            //connection string convention:
            string cnnKey = "contosoOne";
            string? cnn = _configuration.GetConnectionString(cnnKey);
            _logger?.LogInformation("{Key}: {Value}", cnnKey, cnn);

            //binding to a dictionary:
            Dictionary<string, string> set = new();
            _configuration.GetSection("myDictionary").Bind(set);
            foreach (var pair in set)
            {
                _logger?.LogInformation("key: {Key}, value: {Value}", pair.Key, pair.Value);
            }

            _logger?.LogWarning("Stopping application...");

            _exitCode = 0;
            _hostApplicationLifetime.StopApplication();
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("Stopping `{Name}`...", nameof(MyHostedService));

        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        // FUNKYKB: exit code may be null should use enter Ctrl+c/SIGTERM.

        return Task.CompletedTask;
    }

    readonly IConfiguration _configuration;
    readonly IHostApplicationLifetime _hostApplicationLifetime;
    readonly ILogger<MyHostedService> _logger;

    int? _exitCode;
}
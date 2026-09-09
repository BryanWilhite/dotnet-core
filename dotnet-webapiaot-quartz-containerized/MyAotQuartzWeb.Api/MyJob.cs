using Quartz;

namespace MyAotQuartzWeb.Api;

public class MyJob(Todo[] todos, ILogger<MyJob> logger) : IJob
{
    public ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Hello world! There are {Number} TODOs.", todos.Length);

        return ValueTask.CompletedTask;
    }
}

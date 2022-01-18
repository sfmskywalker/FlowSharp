using Elsa.Contracts;
using Elsa.Models;

namespace Elsa.Pipelines.WorkflowExecution.Components;

public static class UseActivitySchedulerMiddlewareExtensions
{
    public static IWorkflowExecutionBuilder UseActivityScheduler(this IWorkflowExecutionBuilder builder) => builder.UseMiddleware<ActivitySchedulerMiddleware>();
}

public class ActivitySchedulerMiddleware : WorkflowExecutionMiddleware
{
    public ActivitySchedulerMiddleware(WorkflowMiddlewareDelegate next) : base(next)
    {
    }

    public override async ValueTask InvokeAsync(WorkflowExecutionContext context)
    {
        var scheduler = context.Scheduler;

        // As long as there are activities scheduled, keep executing them.
        while (scheduler.HasAny)
        {
            // Pop next work item for execution.
            var currentWorkItem = scheduler.Pop();

            // Execute work item.
            await currentWorkItem.Execute();
        }

        // Invoke next middleware.
        await Next(context);
    }
}
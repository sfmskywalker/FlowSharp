using Elsa.Mediator.Services;
using Elsa.Workflows.Core.Helpers;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Core.Pipelines.WorkflowExecution;
using Elsa.Workflows.Runtime.Models;
using Elsa.Workflows.Runtime.Notifications;
using Elsa.Workflows.Runtime.Services;

namespace Elsa.Workflows.Runtime.Middleware;

/// <summary>
/// Takes care of loading & persisting workflow variables.
/// </summary>
public class PersistBookmarkMiddleware : WorkflowExecutionMiddleware
{
    private readonly IWorkflowRuntime _workflowRuntime;
    private readonly IEventPublisher _eventPublisher;

    public PersistBookmarkMiddleware(WorkflowMiddlewareDelegate next, IWorkflowRuntime workflowRuntime, IEventPublisher eventPublisher) : base(next)
    {
        _workflowRuntime = workflowRuntime;
        _eventPublisher = eventPublisher;
    }

    public override async ValueTask InvokeAsync(WorkflowExecutionContext context)
    {
        var cancellationToken = context.CancellationToken;

        // Get current bookmarks.
        var originalBookmarks = context.Bookmarks.ToList();

        // Invoke next middleware.
        await Next(context);

        // Get new bookmarks.
        var updatedBookmarks = context.Bookmarks.ToList();

        // Get a diff.
        var diff = Diff.For(originalBookmarks, updatedBookmarks);

        // Update bookmarks.
        var updateBookmarksContext = new UpdateBookmarksContext(context.Id, diff, context.CorrelationId);
        await _workflowRuntime.UpdateBookmarksAsync(updateBookmarksContext, cancellationToken);

        // Publish domain event.
        await _eventPublisher.PublishAsync(new WorkflowBookmarksIndexed(new IndexedWorkflowBookmarks(context.Id, diff.Added, diff.Removed)), cancellationToken);

        // Notify all interested activities that the bookmarks have been persisted.
        var activityExecutionContexts = context.ActivityExecutionContexts.Where(x => x.Activity is IBookmarksPersistedHandler && x.Bookmarks.Any()).ToList();

        foreach (var activityExecutionContext in activityExecutionContexts) 
            await ((IBookmarksPersistedHandler)activityExecutionContext.Activity).BookmarksPersistedAsync(activityExecutionContext);
    }
}
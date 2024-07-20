using Elsa.Alterations.Core.Enums;
using Elsa.Alterations.Core.Notifications;
using Elsa.Mediator.Contracts;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Requests;
using JetBrains.Annotations;

namespace Elsa.Alterations.Handlers;

/// <summary>
/// Resumes a workflow instance when an alteration job has been completed and the workflow contains scheduled work.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ResumeWorkflowInstance"/> class.
/// </remarks>
[UsedImplicitly]
public class ResumeWorkflowInstance(IWorkflowDispatcher workflowDispatcher) : INotificationHandler<AlterationJobCompleted>
{
    private readonly IWorkflowDispatcher _workflowDispatcher = workflowDispatcher;

    /// <inheritdoc />
    public async Task HandleAsync(AlterationJobCompleted notification, CancellationToken cancellationToken)
    {
        var job = notification.Job;
        
        // Check if job is completed.
        if(job.Status != AlterationJobStatus.Completed)
            return;
        
        // Check if the workflow contains scheduled work.
        if(!notification.WorkflowContainsScheduledWork)
            return;
        
        // Resume workflow instance.
        await _workflowDispatcher.DispatchAsync(new DispatchWorkflowInstanceRequest(job.WorkflowInstanceId), cancellationToken: cancellationToken);
    }
}
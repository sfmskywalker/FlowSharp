namespace Elsa.Alterations.Endpoints.Workflows.Retry;

/// <summary>
/// A plan that contains a list of alterations to be applied to a set of workflow instances.
/// </summary>
public class Request
{
    /// <summary>
    /// The IDs of the workflow instances that have incidents to be retried.
    /// </summary>
    public ICollection<string> WorkflowInstanceIds { get; set; } = new List<string>();

    /// <summary>
    /// An optional list of explicitly specified activity IDs to retry. If omitted, all faulted activities will be retried.
    /// </summary>
    public ICollection<string>? ActivityIds { get; set; }
}
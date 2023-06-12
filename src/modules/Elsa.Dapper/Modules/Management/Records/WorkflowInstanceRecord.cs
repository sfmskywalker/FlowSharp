namespace Elsa.Dapper.Modules.Management.Records;

internal class WorkflowInstanceRecord
{
    public string Id { get; set; } = default!;
    public string DefinitionId { get; set; } = default!;
    public string DefinitionVersionId { get; set; } = default!;
    public int Version { get; set; }
    public string WorkflowState { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string SubStatus { get; set; } = default!;
    public string? CorrelationId { get; set; }
    public string? Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastExecutedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset? FaultedAt { get; set; }
}
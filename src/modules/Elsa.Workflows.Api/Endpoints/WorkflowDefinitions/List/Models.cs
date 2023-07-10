using Elsa.Common.Entities;
using Elsa.Workflows.Api.Models;
using Elsa.Workflows.Management.Models;
using FastEndpoints;

namespace Elsa.Workflows.Api.Endpoints.WorkflowDefinitions.List;

internal class Request
{
    public string? VersionOptions { get; set; }
    public string[]? DefinitionIds { get; set; }
    public string[]? Ids { get; set; }
    [BindFrom("materializer")] public string? MaterializerName { get; set; }
    [BindFrom("label")] public string[]? Labels { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public OrderByWorkflowDefinition? OrderBy { get; set; }
    public OrderDirection? OrderDirection { get; set; }
    public string? SearchTerm { get; set; }
}

internal class Response
{
    public Response(ICollection<WorkflowDefinitionSummary> items, long totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public ICollection<WorkflowDefinitionSummary> Items { get; set; }
    public long TotalCount { get; set; }
}
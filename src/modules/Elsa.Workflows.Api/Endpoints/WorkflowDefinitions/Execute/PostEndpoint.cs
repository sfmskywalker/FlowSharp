using Elsa.Workflows.Contracts;
using Elsa.Workflows.Management;
using Elsa.Workflows.Runtime;
using JetBrains.Annotations;

namespace Elsa.Workflows.Api.Endpoints.WorkflowDefinitions.Execute;

/// <summary>
/// An API endpoint that executes a given workflow definition through POST method.
/// </summary>
[PublicAPI]
internal class PostEndpoint(
    IWorkflowDefinitionService workflowDefinitionService, 
    IWorkflowRuntime workflowRuntime, 
    IApiSerializer apiSerializer) 
    : EndpointBase<PostRequest>(workflowDefinitionService, workflowRuntime, apiSerializer)
{
    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Verbs(FastEndpoints.Http.POST);
    }

    protected override IDictionary<string, object>? GetInput(PostRequest request) => 
        (IDictionary<string, object>?)request.Input;
}
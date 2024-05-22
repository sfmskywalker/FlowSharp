using Elsa.Abstractions;
using Elsa.Common.Models;
using Elsa.Workflows.Api.Constants;
using Elsa.Workflows.Api.Services;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Management.Mappers;
using Elsa.Workflows.Management.Models;
using JetBrains.Annotations;

namespace Elsa.Workflows.Api.Endpoints.WorkflowDefinitions.Retract;

[PublicAPI]
internal class Retract : ElsaEndpoint<Request, WorkflowDefinitionModel>
{
    private readonly IWorkflowDefinitionStore _store;
    private readonly IWorkflowDefinitionPublisher _workflowDefinitionPublisher;
    private readonly WorkflowDefinitionMapper _workflowDefinitionMapper;
    private readonly IWorkflowDefinitionLinkService _linkService;

    public Retract(IWorkflowDefinitionStore store, IWorkflowDefinitionPublisher workflowDefinitionPublisher, WorkflowDefinitionMapper workflowDefinitionMapper, IWorkflowDefinitionLinkService linkService)
    {
        _store = store;
        _workflowDefinitionPublisher = workflowDefinitionPublisher;
        _workflowDefinitionMapper = workflowDefinitionMapper;
        _linkService = linkService;
    }

    public override void Configure()
    {
        Post("/workflow-definitions/{definitionId}/retract");
        ConfigurePermissions("retract:workflow-definitions");
        Policies(AuthorizationPolicies.NotReadOnlyPolicy);
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var filter = new WorkflowDefinitionFilter
        {
            DefinitionId = request.DefinitionId,
            VersionOptions = VersionOptions.LatestOrPublished
        };

        var definition = await _store.FindAsync(filter, cancellationToken);

        if (definition == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (!definition.IsPublished)
        {
            AddError($"Workflow with id {request.DefinitionId} is not published");
            await SendErrorsAsync(cancellation: cancellationToken);
            return;
        }

        await _workflowDefinitionPublisher.RetractAsync(definition, cancellationToken);
        var response = await _workflowDefinitionMapper.MapAsync(definition, cancellationToken);
        response = _linkService.GenerateLinksForSingleEntry(response);
        await SendOkAsync(response, cancellationToken);
    }
}
using Elsa.Common.Models;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.State;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Workflows.Runtime.Services;

/// <inheritdoc />
public class WorkflowHostFactory(IIdentityGenerator identityGenerator, IServiceProvider serviceProvider, IWorkflowDefinitionService workflowDefinitionService)
    : IWorkflowHostFactory
{
    /// <inheritdoc />
    public async Task<IWorkflowHost?> CreateAsync(string definitionId, VersionOptions versionOptions, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var workflowDefinitionService = scope.ServiceProvider.GetRequiredService<IWorkflowDefinitionService>();
        var workflow = await workflowDefinitionService.FindWorkflowGraphAsync(definitionId, versionOptions, cancellationToken);
        
        if(workflow == null)
            return default;

        return await CreateAsync(workflow, cancellationToken);
    }

    /// <inheritdoc />
    public Task<IWorkflowHost> CreateAsync(WorkflowGraph workflowGraph, WorkflowState workflowState, CancellationToken cancellationToken = default)
    {
        var workflowHost = (IWorkflowHost)ActivatorUtilities.CreateInstance<WorkflowHost>(_serviceProvider, workflowGraph, workflowState);
        return Task.FromResult(workflowHost);
    }

    /// <inheritdoc />
    public Task<IWorkflowHost> CreateAsync(WorkflowGraph workflowGraph, CancellationToken cancellationToken = default)
    {
        var workflow = workflowGraph.Workflow;
        var workflowState = new WorkflowState
        {
            Id = _identityGenerator.GenerateId(),
            DefinitionId =workflow.Identity.DefinitionId,
            DefinitionVersion = workflow.Identity.Version
        };

        return CreateAsync(workflowGraph, workflowState, cancellationToken);
    }
}
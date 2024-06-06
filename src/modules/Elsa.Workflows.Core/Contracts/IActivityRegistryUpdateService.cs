namespace Elsa.Workflows.Contracts;

/// <summary>
/// Represents a service for updating the activity registry.
/// </summary>
public interface IActivityRegistryUpdateService
{
    /// <summary>
    /// Tries to add a workflow as an activity to the registry.
    /// </summary>
    /// <param name="providerType">The type of the activity provider.</param>
    /// <param name="workflowDefinitionId">The ID of the workflow definition.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddToRegistry(Type providerType, string workflowDefinitionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes workflow definition activities from the <see cref="IActivityRegistry"/>.
    /// </summary>
    /// <param name="providerType">The type of the Activity Provider.</param>
    /// <param name="workflowDefinitionId">The ID of the workflow definition to remove.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    void RemoveDefinitionFromRegistry(Type providerType, string workflowDefinitionId, CancellationToken cancellationToken = default);
    
    
    /// <summary>
    /// Removes a workflow definition version activity from the <see cref="IActivityRegistry"/>.
    /// </summary>
    /// <param name="providerType">The type of the Activity Provider.</param>
    /// <param name="workflowDefinitionVersionId">The ID of the workflow definition to remove.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    void RemoveDefinitionVersionFromRegistry(Type providerType, string workflowDefinitionVersionId, CancellationToken cancellationToken = default);
}
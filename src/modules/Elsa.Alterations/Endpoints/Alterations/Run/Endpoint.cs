using Elsa.Abstractions;
using Elsa.Alterations.Core.Contracts;
using JetBrains.Annotations;

namespace Elsa.Alterations.Endpoints.Alterations.Run;

/// <summary>
/// Executes an alteration plan.
/// </summary>
/// <inheritdoc />
[PublicAPI]
public class Run(IAlterationRunner alterationRunner, IAlteredWorkflowDispatcher workflowDispatcher) : ElsaEndpoint<Request, Response>
{
    private readonly IAlterationRunner _alterationRunner = alterationRunner;
    private readonly IAlteredWorkflowDispatcher _workflowDispatcher = workflowDispatcher;

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/alterations/run");
        ConfigurePermissions("run:alterations");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        // Run the alterations.
        var results = await _alterationRunner.RunAsync(request.WorkflowInstanceIds, request.Alterations, cancellationToken);

        // Schedule each successfully updated workflow containing scheduled work.
        await _workflowDispatcher.DispatchAsync(results, cancellationToken);

        // Write response.
        var response = new Response(results);
        await SendOkAsync(response, cancellationToken);
    }
}
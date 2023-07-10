using Elsa.Common.Models;
using Elsa.ProtoActor.Extensions;
using Elsa.ProtoActor.Mappers;
using Elsa.ProtoActor.Protos;
using Elsa.Workflows.Core.Contracts;
using Elsa.Workflows.Core.State;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Runtime.Contracts;
using Proto;
using Proto.Cluster;
using Proto.Persistence;
using Exception = System.Exception;
using WorkflowStatus = Elsa.Workflows.Core.WorkflowStatus;
using ProtoBookmark = Elsa.ProtoActor.Protos.Bookmark;

namespace Elsa.ProtoActor.Grains;

/// <summary>
/// Executes a workflow.
/// </summary>
public class WorkflowGrain : WorkflowGrainBase
{
    private const int MaxSnapshotsToKeep = 5;
    private readonly IWorkflowDefinitionService _workflowDefinitionService;
    private readonly IWorkflowHostFactory _workflowHostFactory;
    private readonly IWorkflowStateSerializer _workflowStateSerializer;
    private readonly BookmarkMapper _bookmarkMapper;
    private readonly WorkflowStatusMapper _workflowStatusMapper;
    private readonly WorkflowSubStatusMapper _workflowSubStatusMapper;
    private readonly WorkflowFaultStateMapper _workflowFaultStateMapper;
    private readonly Persistence _persistence;

    private string _definitionId = default!;
    private string _instanceId = default!;
    private int _version;
    private IDictionary<string, object>? _input;
    private IWorkflowHost _workflowHost = default!;
    private WorkflowState _workflowState = default!;

    /// <inheritdoc />
    public WorkflowGrain(
        IWorkflowDefinitionService workflowDefinitionService,
        IWorkflowHostFactory workflowHostFactory,
        IWorkflowStateSerializer workflowStateSerializer,
        IProvider provider,
        IContext context,
        BookmarkMapper bookmarkMapper,
        WorkflowStatusMapper workflowStatusMapper,
        WorkflowSubStatusMapper workflowSubStatusMapper,
        WorkflowFaultStateMapper workflowFaultStateMapper) : base(context)
    {
        _workflowDefinitionService = workflowDefinitionService;
        _workflowHostFactory = workflowHostFactory;
        _workflowStateSerializer = workflowStateSerializer;
        _bookmarkMapper = bookmarkMapper;
        _workflowStatusMapper = workflowStatusMapper;
        _workflowSubStatusMapper = workflowSubStatusMapper;
        _workflowFaultStateMapper = workflowFaultStateMapper;
        _persistence = Persistence.WithSnapshotting(provider, Context.ClusterIdentity()!.Identity, ApplySnapshot);
    }

    /// <inheritdoc />
    public override async Task OnStarted()
    {
        await _persistence.RecoverStateAsync();

        if (string.IsNullOrWhiteSpace(_definitionId))
            return; // No state yet to recover from.

        var cancellationToken = Context.CancellationToken;

        // Load the workflow definition.
        var workflowDefinition = await _workflowDefinitionService.FindAsync(_definitionId, VersionOptions.SpecificVersion(_version), cancellationToken);

        if (workflowDefinition == null)
            throw new Exception("Workflow definition is no longer available");

        // Materialize the workflow.
        var workflow = await _workflowDefinitionService.MaterializeWorkflowAsync(workflowDefinition, cancellationToken);

        // Create an initial workflow state.
        if (_workflowState == null!)
        {
            _workflowState = new WorkflowState
            {
                DefinitionId = workflow.Identity.DefinitionId,
                DefinitionVersion = workflow.Identity.Version,
            };
        }

        // Create a workflow host.
        _workflowHost = await _workflowHostFactory.CreateAsync(workflow, _workflowState, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<CanStartWorkflowResponse> CanStart(StartWorkflowRequest request)
    {
        var definitionId = request.DefinitionId;
        var instanceId = request.InstanceId;
        var correlationId = request.CorrelationId.NullIfEmpty();
        var input = request.Input?.Deserialize();
        var versionOptions = VersionOptions.FromString(request.VersionOptions);
        var cancellationToken = Context.CancellationToken;
        var startWorkflowOptions = new StartWorkflowHostOptions(instanceId, correlationId, input, request.TriggerActivityId);

        _workflowHost = await CreateWorkflowHostAsync(definitionId, versionOptions, cancellationToken);
        _version = _workflowHost.Workflow.Identity.Version;
        _definitionId = definitionId;
        _instanceId = instanceId;
        _input = input;

        var canStart = await _workflowHost.CanStartWorkflowAsync(startWorkflowOptions, cancellationToken);

        return new CanStartWorkflowResponse
        {
            CanStart = canStart
        };
    }

    /// <inheritdoc />
    public override async Task<WorkflowExecutionResponse> Start(StartWorkflowRequest request)
    {
        var definitionId = request.DefinitionId;
        var instanceId = request.InstanceId;
        var correlationId = request.CorrelationId.NullIfEmpty();
        var input = request.Input?.Deserialize();
        var versionOptions = VersionOptions.FromString(request.VersionOptions);
        var cancellationToken = Context.CancellationToken;

        // Only need to reconstruct a workflow host if not already done so during CanStart.
        if (_workflowHost == null!)
        {
            _workflowHost = await CreateWorkflowHostAsync(definitionId, versionOptions, cancellationToken);
            _version = _workflowHost.Workflow.Identity.Version;
            _definitionId = definitionId;
            _instanceId = instanceId;
            _input = input;
        }

        var startWorkflowOptions = new StartWorkflowHostOptions(instanceId, correlationId, input, request.TriggerActivityId);
        await _workflowHost.StartWorkflowAsync(startWorkflowOptions, cancellationToken);
        var workflowState = _workflowHost.WorkflowState;
        var result = workflowState.Status == WorkflowStatus.Finished ? Protos.RunWorkflowResult.Finished : Protos.RunWorkflowResult.Suspended;

        _workflowState = workflowState;

        await SaveSnapshotAsync();

        return new WorkflowExecutionResponse
        {
            Result = result,
            Bookmarks = { _bookmarkMapper.Map(workflowState.Bookmarks).ToList() },
            Status = _workflowStatusMapper.Map(workflowState.Status),
            SubStatus = _workflowSubStatusMapper.Map(workflowState.SubStatus),
            Fault = workflowState.Fault != null ? _workflowFaultStateMapper.Map(workflowState.Fault) : default,
            TriggeredActivityId = string.Empty,
            WorkflowInstanceId = instanceId
        };
    }

    /// <inheritdoc />
    public override async Task<WorkflowExecutionResponse> Resume(ResumeWorkflowRequest request)
    {
        _input = request.Input?.Deserialize();
        var correlationId = request.CorrelationId;
        var bookmarkId = request.BookmarkId.NullIfEmpty();
        var activityId = request.ActivityId.NullIfEmpty();
        var activityNodeId = request.ActivityNodeId.NullIfEmpty();
        var activityInstanceId = request.ActivityInstanceId.NullIfEmpty();
        var activityHash = request.ActivityHash.NullIfEmpty();
        var cancellationToken = Context.CancellationToken;

        var resumeWorkflowHostOptions = new ResumeWorkflowHostOptions(
            correlationId,
            bookmarkId,
            activityId,
            activityNodeId,
            activityInstanceId,
            activityHash,
            _input);

        var definitionId = _definitionId;
        var versionOptions = VersionOptions.SpecificVersion(_version);

        // Only need to reconstruct a workflow host if not already done so during CanStart.
        if (_workflowHost == null!)
        {
            _workflowHost = await CreateWorkflowHostAsync(definitionId, versionOptions, cancellationToken);
            _version = _workflowHost.Workflow.Identity.Version;
        }

        await _workflowHost.ResumeWorkflowAsync(resumeWorkflowHostOptions, cancellationToken);
        var finished = _workflowHost.WorkflowState.Status == WorkflowStatus.Finished;

        _workflowState = _workflowHost.WorkflowState;

        await SaveSnapshotAsync();

        return new WorkflowExecutionResponse
        {
            Result = finished ? RunWorkflowResult.Finished : RunWorkflowResult.Suspended,
            Bookmarks = { _bookmarkMapper.Map(_workflowHost.WorkflowState.Bookmarks).ToList() },
            Fault = _workflowState.Fault != null ? _workflowFaultStateMapper.Map(_workflowState.Fault) : default,
            TriggeredActivityId = string.Empty,
            WorkflowInstanceId = _workflowState.Id,
            Status = _workflowStatusMapper.Map(_workflowState.Status),
            SubStatus = _workflowSubStatusMapper.Map(_workflowState.SubStatus)
        };
    }

    /// <inheritdoc />
    public override async Task<ExportWorkflowStateResponse> ExportState(ExportWorkflowStateRequest request)
    {
        var json = await _workflowStateSerializer.SerializeAsync(_workflowHost.WorkflowState);

        var response = new ExportWorkflowStateResponse
        {
            SerializedWorkflowState = new Json
            {
                Text = json
            }
        };

        return response;
    }

    /// <inheritdoc />
    public override async Task<ImportWorkflowStateResponse> ImportState(ImportWorkflowStateRequest request)
    {
        var workflowState = await _workflowStateSerializer.DeserializeAsync(request.SerializedWorkflowState.Text);

        _workflowState = workflowState;
        _workflowHost.WorkflowState = workflowState;
        _definitionId = workflowState.DefinitionId;
        _instanceId = workflowState.Id;
        _version = workflowState.DefinitionVersion;
        _workflowHost = await CreateWorkflowHostAsync(workflowState, Context.CancellationToken);

        return new ImportWorkflowStateResponse();
    }

    private void ApplySnapshot(Snapshot snapshot) => (_definitionId, _instanceId, _version, _workflowState, _input) = (WorkflowSnapshot)snapshot.State;

    private async Task SaveSnapshotAsync()
    {
        if (_workflowState.Status == WorkflowStatus.Finished)
            // If the workflow has finished, delete all snapshots.
            await _persistence.DeleteSnapshotsAsync(_persistence.Index);
        else
            // Otherwise, create a new snapshot, automatically deleting the last N snapshots. 
            await _persistence.PersistRollingSnapshotAsync(GetState(), MaxSnapshotsToKeep);
    }

    private object GetState() => new WorkflowSnapshot(_definitionId, _instanceId, _version, _workflowState, _input);

    private async Task<IWorkflowHost> CreateWorkflowHostAsync(string definitionId, VersionOptions versionOptions, CancellationToken cancellationToken)
    {
        var workflowDefinition = await _workflowDefinitionService.FindAsync(definitionId, versionOptions, cancellationToken);

        if (workflowDefinition == null)
            throw new Exception("Specified workflow definition and version does not exist");

        var workflow = await _workflowDefinitionService.MaterializeWorkflowAsync(workflowDefinition, cancellationToken);
        return await _workflowHostFactory.CreateAsync(workflow, cancellationToken);
    }
    
    private async Task<IWorkflowHost> CreateWorkflowHostAsync(WorkflowState workflowState, CancellationToken cancellationToken)
    {
        var definitionId = workflowState.DefinitionId;
        var versionOptions = VersionOptions.SpecificVersion(workflowState.DefinitionVersion);
        var workflowDefinition = await _workflowDefinitionService.FindAsync(definitionId, versionOptions, cancellationToken);

        if (workflowDefinition == null)
            throw new Exception("Specified workflow definition and version does not exist");

        var workflow = await _workflowDefinitionService.MaterializeWorkflowAsync(workflowDefinition, cancellationToken);
        return await _workflowHostFactory.CreateAsync(workflow, workflowState, cancellationToken);
    }
}
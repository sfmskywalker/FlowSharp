using Elsa.Alterations.Core.Contracts;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Activities;
using Elsa.Workflows.Management.Entities;
using Microsoft.Extensions.Logging;

namespace Elsa.Alterations.Core.Contexts;

/// <summary>
/// Provides contextual information about an alteration.
/// </summary>
public class AlterationContext
{
    private readonly IAlteration _alteration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlterationContext"/> class.
    /// </summary>
    public AlterationContext(
        string batchId, 
        IAlteration alteration, 
        WorkflowExecutionContext workflowExecutionContext, 
        IAlterationLog log, 
        IServiceProvider serviceProvider, 
        CancellationToken cancellationToken)
    {
        _alteration = alteration;
        BatchId = batchId;
        WorkflowExecutionContext = workflowExecutionContext;
        AlterationLog = log;
        ServiceProvider = serviceProvider;
        CancellationToken = cancellationToken;
    }

    /// <summary>
    /// The Id of the batch that this alteration belongs to.
    /// </summary>
    public string BatchId { get; }

    /// <summary>
    /// A workflow execution context of the workflow instance being altered. This offers maximum flexibility for altering the workflow state.
    /// </summary>
    public WorkflowExecutionContext WorkflowExecutionContext { get; }

    /// <summary>
    /// The workflow of the workflow instance being altered.
    /// </summary>
    public Workflow Workflow => WorkflowExecutionContext.Workflow;

    /// <summary>
    /// The cancellation token.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// The service provider.
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// The alteration log.
    /// </summary>
    public IAlterationLog AlterationLog { get; }
    
    /// <summary>
    /// A flag indicating whether the alteration has succeeded.
    /// </summary>
    public bool HasSucceeded { get; private set; }
    
    /// <summary>
    /// A flag indicating whether the alteration has failed.
    /// </summary>
    public bool HasFailed { get; private set; }
    
    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="logLevel">The log level.</param>
    public void Log(string message, LogLevel logLevel = LogLevel.Information)
    {
        AlterationLog.Log(BatchId, _alteration.Id, message, logLevel);
    }

    /// <summary>
    /// Marks the alteration as succeeded.
    /// </summary>
    public void Succeed(string? message = default)
    {
        HasSucceeded = true;
        Log(message ?? "Succeeded", LogLevel.Information);
    }

    /// <summary>
    /// Marks the alteration as failed.
    /// </summary>
    /// <param name="message">An optional message.</param>
    public void Fail(string? message = default)
    {
        HasFailed = true;
        Log(message ?? "Failed", LogLevel.Error);
    }
}
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Elsa.Extensions;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;

namespace Elsa.WorkflowContexts.Activities;

/// <summary>
/// Sets a workflow context parameter for a given workflow context provider.
/// </summary>
[Activity("Elsa", "Primitives", "Sets a workflow context parameter for a given workflow context provider.")]
public class SetWorkflowContextParameter : CodeActivity
{
    /// <inheritdoc />
    [JsonConstructor]
    public SetWorkflowContextParameter([CallerFilePath] string? source = default, [CallerLineNumber] int? line = default) : base(source, line)
    {
    }
    
    /// <summary>
    /// The type of the workflow context provider.
    /// </summary>
    [Input(
        UIHint = "workflow-context-provider-picker",
        Description = "The type of the workflow context provider."
    )]
    public Input<Type> ProviderType { get; set; } = default!;
    
    /// <summary>
    /// The name of the parameter to set. If not specified, the parameter name will be inferred from the workflow context provider.
    /// </summary>
    [Input(Description = "Optional. The name of the parameter to set. If not specified, the parameter name will be inferred from the workflow context provider.")]
    public Input<string?> ParameterName { get; set; } = default!;

    /// <summary>
    /// The value of the parameter to set.
    /// </summary>
    [Input(Description = "The value of the parameter to set.")]
    public Input<object> ParameterValue { get; set; } = default!;

    /// <inheritdoc />
    protected override void Execute(ActivityExecutionContext context)
    {
        var providerType = ProviderType.Get(context);
        var parameterName = ParameterName.GetOrDefault(context);
        var scopedParameterName = providerType.GetScopedParameterName(parameterName);
        var parameterValue = ParameterValue.Get(context);

        context.WorkflowExecutionContext.SetProperty(scopedParameterName, parameterValue);
    }
}
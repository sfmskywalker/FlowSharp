using System.Dynamic;
using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.JavaScript.Helpers;
using Elsa.JavaScript.Notifications;
using Elsa.Mediator.Contracts;
using Elsa.Workflows.Activities;
using JetBrains.Annotations;
using Jint;
using Jint.Native;

namespace Elsa.JavaScript.Handlers;

/// A handler that configures the Jint engine with workflow variables.
[UsedImplicitly]
public class ConfigureEngineWithVariables : INotificationHandler<EvaluatingJavaScript>, INotificationHandler<EvaluatedJavaScript>
{
    /// <inheritdoc />
    public Task HandleAsync(EvaluatingJavaScript notification, CancellationToken cancellationToken)
    {
        CopyVariablesIntoEngine(notification);
        return Task.CompletedTask;
    }

    public Task HandleAsync(EvaluatedJavaScript notification, CancellationToken cancellationToken)
    {
        CopyVariablesIntoWorkflowExecutionContext(notification);
        return Task.CompletedTask;
    }

    private void CopyVariablesIntoWorkflowExecutionContext(EvaluatedJavaScript notification)
    {
        var context = notification.Context;
        var engine = notification.Engine;
        var variablesContainer = (IDictionary<string, object?>)engine.GetValue("variables").ToObject()!;
        var inputNames = GetInputNames(context).Distinct().ToList();

        foreach (var (variableName, variableValue) in variablesContainer)
        {
            if (inputNames.Contains(variableName))
                continue;

            var processedValue = variableValue is JsObject jsValue ? jsValue.ToObject() : variableValue ?? context.GetVariableInScope(variableName);
            context.SetVariable(variableName, processedValue);
        }
    }

    private IEnumerable<string> GetInputNames(ExpressionExecutionContext context)
    {
        var activityExecutionContext = context.TryGetActivityExecutionContext(out var aec) ? aec : null;

        while (activityExecutionContext != null)
        {
            if (activityExecutionContext.Activity is Workflow workflow)
            {
                var inputDefinitions = workflow.Inputs;

                foreach (var inputDefinition in inputDefinitions)
                    yield return inputDefinition.Name;
            }

            activityExecutionContext = activityExecutionContext.ParentActivityExecutionContext;
        }
    }

    private void CopyVariablesIntoEngine(EvaluatingJavaScript notification)
    {
        var engine = notification.Engine;
        var context = notification.Context;
        var variableNames = context.GetVariableNamesInScope().ToList();
        var variablesContainer = (IDictionary<string, object?>)new ExpandoObject();

        foreach (var variableName in variableNames)
        {
            var variableValue = context.GetVariableInScope(variableName);
            variableValue = ProcessVariableValue(engine, variableValue);
            variablesContainer[variableName] = variableValue;
        }

        engine.SetValue("variables", variablesContainer);
    }

    private object? ProcessVariableValue(Engine engine, object? variableValue)
    {
        if (variableValue == null)
            return null;

        if (variableValue is not ExpandoObject expandoObject)
            return variableValue;

        return ObjectConverterHelper.ConvertToJsObject(engine, expandoObject);
    }
}
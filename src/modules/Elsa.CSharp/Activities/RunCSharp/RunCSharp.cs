﻿using System.Runtime.CompilerServices;
using Elsa.CSharp.Contracts;
using Elsa.CSharp.Extensions;
using Elsa.CSharp.Models;
using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;

// ReSharper disable once CheckNamespace
namespace Elsa.CSharp.Activities;

/// <summary>
/// Executes C# code.
/// </summary>
[Activity("Elsa", "Scripting", "Executes C# code", DisplayName = "Run C#")]
public class RunCSharp : CodeActivity<object?>
{
    /// <inheritdoc />
    public RunCSharp([CallerFilePath] string? source = default, [CallerLineNumber] int? line = default) : base(source, line)
    {
    }

    /// <inheritdoc />
    public RunCSharp(string script, [CallerFilePath] string? source = default, [CallerLineNumber] int? line = default) : this(source, line)
    {
        Script = new Input<string>(script);
    }
    
    /// <summary>
    /// A list of possible outcomes. Use "SetOutcome(string)" to set the outcome. Use "SetOutcomes(params string[])" to set multiple outcomes.
    /// </summary>
    [Input(Description = "A list of possible outcomes.", UIHint = InputUIHints.DynamicOutcomes)]
    public Input<ICollection<string>> PossibleOutcomes { get; set; } = default!;

    /// <summary>
    /// The script to run.
    /// </summary>
    [Input(
        Description = "The script to run.",
        UIHint = InputUIHints.CodeEditor,
        OptionsProvider = typeof(RunCSharpOptionsProvider)
    )]
    public Input<string> Script { get; set; } = new("");

    /// <inheritdoc />
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var script = context.Get(Script);

        // If no script was specified, there's nothing to do.
        if (string.IsNullOrWhiteSpace(script))
            return;

        // Get a JavaScript evaluator.
        var evaluator = context.GetRequiredService<ICSharpEvaluator>();

        // Run the script.
        var result = await evaluator.EvaluateAsync(script, typeof(object), context.ExpressionExecutionContext, context.CancellationToken);

        // Set the result as output, if any.
        if (result is not null)
            context.Set(Result, result);

        // Get the outcome or outcomes set by the script, if any. If not set, use "Done".
        var outcomes = context.ExpressionExecutionContext.TransientProperties.GetValueOrDefault(Globals.OutcomePropertiesKey, () => new[] { "Done" })!;

        // Complete the activity with the outcome.
        await context.CompleteActivityWithOutcomesAsync(outcomes);
    }
}
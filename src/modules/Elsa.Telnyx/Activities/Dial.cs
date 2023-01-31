﻿using System.Text.Json.Serialization;
using Elsa.Extensions;
using Elsa.Telnyx.Client.Models;
using Elsa.Telnyx.Client.Services;
using Elsa.Telnyx.Exceptions;
using Elsa.Telnyx.Extensions;
using Elsa.Telnyx.Options;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;
using Elsa.Workflows.Management.Models;
using Microsoft.Extensions.Options;

namespace Elsa.Telnyx.Activities;

/// <summary>
/// Dial a number or SIP URI.
/// </summary>
[Activity(Constants.Namespace, "Dial a number or SIP URI.", Kind = ActivityKind.Task)]
public class Dial : CodeActivity<DialResponse>
{
    /// <inheritdoc />
    [JsonConstructor]
    public Dial(string? source = default, int? line = default) : base(source, line)
    {
    }
    
    /// <summary>
    /// The DID or SIP URI to dial out and bridge to the given call.
    /// </summary>
    [Input(Description = "The DID or SIP URI to dial out and bridge to the given call.")]
    public Input<string> To { get; set; } = default!;

    /// <summary>
    /// The 'from' number to be used as the caller id presented to the destination ('To' number). The number should be in +E164 format. This attribute will default to the 'From' number of the original call if omitted.
    /// </summary>
    [Input(Description = "The 'from' number to be used as the caller id presented to the destination ('To' number). The number should be in +E164 format. This attribute will default to the 'From' number of the original call if omitted.")]
    public Input<string?> From { get; set; } = default!;

    /// <summary>
    /// The string to be used as the caller id name (SIP From Display Name) presented to the destination ('To' number). The string should have a maximum of 128 characters, containing only letters, numbers, spaces, and -_~!.+ special characters. If omitted, the display name will be the same as the number in the 'From' field.
    /// </summary>
    [Input(Description =
        "The string to be used as the caller id name (SIP From Display Name) presented to the destination ('To' number). The string should have a maximum of 128 characters, containing only letters, numbers, spaces, and -_~!.+ special characters. If omitted, the display name will be the same as the number in the 'From' field.")]
    public Input<string?> FromDisplayName { get; set; } = default!;

    /// <summary>
    /// Enables answering machine detection.
    /// </summary>
    [Input(
        Description = "Enables answering machine detection.",
        UIHint = InputUIHints.Dropdown,
        Options = new[] { "disabled", "detect", "detect_beep", "detect_words", "greeting_end", "premium" },
        DefaultValue = "disabled")]
    public Input<string?> AnsweringMachineDetection { get; set; } = new("disabled");

    /// <inheritdoc />
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var response = await DialAsync(context);
        Result.Set(context, response);
    }
    
    private async Task<DialResponse> DialAsync(ActivityExecutionContext context)
    {
        var telnyxOptions = context.GetRequiredService<IOptions<TelnyxOptions>>().Value;
        var callControlAppId = telnyxOptions.CallControlAppId;

        if (callControlAppId == null)
            throw new MissingCallControlAppIdException("No Call Control ID configured");

        var fromNumber = context.Get(From);
        var clientState = context.CreateCorrelatingClientState();

        var request = new DialRequest(
            callControlAppId,
            To.Get(context),
            fromNumber,
            FromDisplayName.Get(context).SanitizeCallerName(),
            AnsweringMachineDetection.Get(context),
            ClientState: clientState
        );

        var telnyxClient = context.GetRequiredService<ITelnyxClient>();
        var response = await telnyxClient.Calls.DialAsync(request, context.CancellationToken);

        return response.Data;
    }
}
﻿using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;

namespace Elsa.FileStorage.Activities;

/// <summary>
/// Save a file to the configured storage provider.
/// </summary>
[Activity("Elsa", "Storage", "Open a file from the configured storage provider.", Kind = ActivityKind.Task)]
public class OpenFile : CodeActivity<Stream>
{
    /// <summary>
    /// Gets or sets the path to save the file to.
    /// </summary>
    public Input<string> Path { get; set; } = default!;

    /// <inheritdoc />
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var cancellationToken = context.CancellationToken;
        var path = Path.Get(context);
        var blobStorageProvider = context.GetRequiredService<IBlobStorageProvider>();
        var blobStorage = blobStorageProvider.GetBlobStorage();
        var data = await blobStorage.OpenReadAsync(path, cancellationToken);
        
        Result.Set(context, data);
    }
}
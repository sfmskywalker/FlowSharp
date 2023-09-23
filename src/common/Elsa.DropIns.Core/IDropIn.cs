﻿using Elsa.Features.Services;

namespace Elsa.DropIns.Core;

/// <summary>
/// Implement this in your drop-in module to configure elsa and register services.
/// </summary>
public interface IDropIn
{
    void Install(IModule module);
    ValueTask ConfigureAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}
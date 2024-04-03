using Elsa.Caching.Distributed.MassTransit.Consumers;
using Elsa.Caching.Distributed.MassTransit.Services;
using Elsa.Caching.Features;
using Elsa.Extensions;
using Elsa.Features.Abstractions;
using Elsa.Features.Attributes;
using Elsa.Features.Services;
using Elsa.MassTransit.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Caching.Distributed.MassTransit.Features;

/// <summary>
/// Configures distributed cache management with MassTransit.
/// </summary>
[DependsOn(typeof(DistributedCacheFeature))]
[DependsOn(typeof(MassTransitFeature))]
public class MassTransitDistributedCacheFeature(IModule module) : FeatureBase(module)
{
    /// <inheritdoc />
    public override void Configure()
    {
        Module.AddMassTransitConsumer<TriggerChangeTokenSignalConsumer>("elsa-trigger-change-token-signal", true);
    }

    /// <inheritdoc />
    public override void Apply()
    {
        Services.AddScoped<MassTransitChangeTokenSignalPublisher>();
    }
}
using Elsa.Agents.Persistence.Entities;
using Elsa.Agents.Persistence.Features;
using Elsa.EntityFrameworkCore.Common;
using Elsa.EntityFrameworkCore.Common.Contracts;
using Elsa.Features.Attributes;
using Elsa.Features.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Agents.Persistence.EntityFrameworkCore;

/// <summary>
/// Configures the default workflow runtime to use EF Core persistence providers.
/// </summary>
[DependsOn(typeof(AgentPersistenceFeature))]
public class EFCoreAgentPersistenceFeature(IModule module) : PersistenceFeatureBase<EFCoreAgentPersistenceFeature, AgentsElsaDbContext>(module)
{
    /// <inheritdoc />
    public override void Configure()
    {
        Module.Configure<AgentPersistenceFeature>(feature =>
        {
            feature.UseApiKeyStore(sp => sp.GetRequiredService<EFCoreApiKeyStore>());
        });
    }

    /// <inheritdoc />
    public override void Apply()
    {
        base.Apply();
        AddEntityStore<ApiKeyDefinition, EFCoreApiKeyStore>();
        Services.AddScoped<IEntityModelCreatingHandler, SetupForOracle>();
    }
}
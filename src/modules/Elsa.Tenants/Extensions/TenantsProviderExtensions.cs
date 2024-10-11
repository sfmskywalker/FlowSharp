using Elsa.Common.Multitenancy;
using Elsa.Tenants.Models;
using JetBrains.Annotations;

namespace Elsa.Tenants.Extensions;

[UsedImplicitly]
public static class TenantsProviderExtensions
{
    public static async Task<Tenant?> FindByIdAsync(this ITenantsProvider tenantsProvider, string id, CancellationToken cancellationToken = default)
    {
        var filter = new TenantFilter
        {
            Id = id
        };
        return await tenantsProvider.FindAsync(filter, cancellationToken);
    }
}
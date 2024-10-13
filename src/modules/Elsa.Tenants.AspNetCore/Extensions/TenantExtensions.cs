using Elsa.Common.Multitenancy;

namespace Elsa.Tenants.AspNetCore;

public static class TenantExtensions
{
    public static string? GetRoutePrefix(this Tenant tenant)
    {
        return tenant.Configuration.GetSection("Http")["Prefix"];
    }
}
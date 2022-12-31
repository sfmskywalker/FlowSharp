using Elsa.Http.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Elsa.Http.Extensions;

/// <summary>
/// Adds extension methods to <see cref="IApplicationBuilder"/> related to workflow middleware components.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Installs the <see cref="WorkflowsMiddleware"/> component.
    /// </summary>
    public static IApplicationBuilder UseWorkflows(this IApplicationBuilder app) => app.UseMiddleware<WorkflowsMiddleware>();
}
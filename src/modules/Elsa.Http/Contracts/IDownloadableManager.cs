using Elsa.Http.Models;
using Elsa.Http.Options;

namespace Elsa.Http.Contracts;

/// <summary>
/// Provides downloadables from the specified content, if supported.
/// </summary>
public interface IDownloadableManager
{
    /// <summary>
    /// Returns a list of downloadables from the specified content.
    /// </summary>
    ValueTask<IEnumerable<Downloadable>> GetDownloadablesAsync(object content, DownloadableOptions? options = default, CancellationToken cancellationToken = default);
}
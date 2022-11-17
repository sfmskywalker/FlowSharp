using Elsa.Workflows.Core.Models;

namespace Elsa.Workflows.Runtime.Services;

/// <summary>
/// Can be implemented by activities that need to do work after their bookmarks have been persisted.
/// </summary>
public interface IBookmarksPersistedHandler
{
    /// <summary>
    /// Invoked when bookmarks have been persisted.
    /// </summary>
    ValueTask BookmarksPersistedAsync(ActivityExecutionContext context);
}
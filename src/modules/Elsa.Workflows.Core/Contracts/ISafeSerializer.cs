namespace Elsa.Workflows.Core.Contracts;

/// <summary>
/// Serializes and deserializes activity state. Only primitive and serializable values are supported.
/// </summary>
public interface ISafeSerializer
{ 
    /// <summary>
    /// Serializes the specified state.
    /// </summary>
    ValueTask<string> SerializeAsync(object? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deserializes the specified state.
    /// </summary>
    ValueTask<T> DeserializeAsync<T>(string json, CancellationToken cancellationToken = default);
}
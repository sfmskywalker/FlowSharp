using Elastic.Clients.Elasticsearch;

namespace Elsa.Elasticsearch.Services;

/// <summary>
/// Implement this interface to get a chance to configure some aspect of Elasticsearch, such as index mappings.
/// </summary>
public interface IElasticConfiguration
{
    /// <summary>
    /// The document type to configure.
    /// </summary>
    Type DocumentType { get; }
    
    /// <summary>
    /// The index naming strategy to use for rollovers.
    /// </summary>
    IIndexNamingStrategy IndexNamingStrategy { get; }
    
    /// <summary>
    /// Invoked by the system to configure <see cref="settings"/>.
    /// </summary>
    /// <param name="settings">The settings to configure.</param>
    void ConfigureClientSettings(ElasticsearchClientSettings settings);

    /// <summary>
    /// Invoked by the system to configure the <see cref="client"/>.
    /// </summary>
    /// <param name="client">The <see cref="ElasticsearchClient"/></param> to configure.
    /// <param name="cancellationToken">A cancellation token.</param>
    ValueTask ConfigureClientAsync(ElasticsearchClient client, CancellationToken cancellationToken);
}
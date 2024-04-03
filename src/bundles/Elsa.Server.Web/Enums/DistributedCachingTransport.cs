namespace Elsa.Server.Web;

/// <summary>
/// Represents the transport options for distributed caching.
/// </summary>
public enum DistributedCachingTransport
{
    Memory,
    Redis,
    MassTransit
}
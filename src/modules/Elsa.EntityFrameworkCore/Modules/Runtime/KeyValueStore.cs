using Elsa.EntityFrameworkCore.Common;
using Elsa.KeyValues.Contracts;
using Elsa.KeyValues.Entities;
using Elsa.KeyValues.Models;
using JetBrains.Annotations;

namespace Elsa.EntityFrameworkCore.Modules.Runtime;

/// Entity Framework implementation of the <see cref="IKeyValueStore"/>
[UsedImplicitly]
public class EFCoreKeyValueStore(Store<RuntimeElsaDbContext, SerializedKeyValuePair> store) : IKeyValueStore
{
    /// <inheritdoc />
    public Task SaveAsync(SerializedKeyValuePair keyValuePair, CancellationToken cancellationToken) => store.SaveAsync(keyValuePair, x => x.Id, cancellationToken);

    /// <inheritdoc />
    public Task<SerializedKeyValuePair?> FindAsync(KeyValueFilter filter, CancellationToken cancellationToken) => store.FindAsync(filter.Apply, cancellationToken);

    /// <inheritdoc />
    public Task<IEnumerable<SerializedKeyValuePair>> FindManyAsync(KeyValueFilter filter, CancellationToken cancellationToken) => store.QueryAsync(filter.Apply, cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(string key, CancellationToken cancellationToken) => store.DeleteWhereAsync(x => x.Id == key, cancellationToken);
}
using Elsa.Common.Models;
using Elsa.Extensions;
using Elsa.MongoDb.Common;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Entities;
using Elsa.Workflows.Runtime.Filters;
using Elsa.Workflows.Runtime.OrderDefinitions;
using MongoDB.Driver.Linq;
using Open.Linq.AsyncExtensions;

namespace Elsa.MongoDb.Modules.Runtime;

/// <inheritdoc />
public class MongoWorkflowExecutionLogStore(MongoDbStore<WorkflowExecutionLogRecord> mongoDbStore) : IWorkflowExecutionLogStore
{
    /// <inheritdoc />
    public async Task AddAsync(WorkflowExecutionLogRecord record, CancellationToken cancellationToken = default)
    {
        await mongoDbStore.AddAsync(record, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddManyAsync(IEnumerable<WorkflowExecutionLogRecord> records, CancellationToken cancellationToken = default)
    {
        await mongoDbStore.AddManyAsync(records, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveAsync(WorkflowExecutionLogRecord record, CancellationToken cancellationToken = default)
    {
        await mongoDbStore.SaveAsync(record, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveManyAsync(IEnumerable<WorkflowExecutionLogRecord> records, CancellationToken cancellationToken = default)
    {
        await mongoDbStore.SaveManyAsync(records, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<WorkflowExecutionLogRecord?> FindAsync(WorkflowExecutionLogRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return await mongoDbStore.FindAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<WorkflowExecutionLogRecord?> FindAsync<TOrderBy>(WorkflowExecutionLogRecordFilter filter, WorkflowExecutionLogRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        return await mongoDbStore.FindAsync(queryable => Order(Filter(queryable, filter), order), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Page<WorkflowExecutionLogRecord>> FindManyAsync(WorkflowExecutionLogRecordFilter filter, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = await mongoDbStore.CountAsync(queryable => Filter(queryable, filter).OrderBy(x => x.Timestamp), cancellationToken);
        var results = await mongoDbStore.FindManyAsync(queryable => Paginate(Filter(queryable, filter), pageArgs), cancellationToken).ToList();
        return new Page<WorkflowExecutionLogRecord>(results, count);
    }

    /// <inheritdoc />
    public async Task<Page<WorkflowExecutionLogRecord>> FindManyAsync<TOrderBy>(WorkflowExecutionLogRecordFilter filter, PageArgs pageArgs, WorkflowExecutionLogRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        var count = await mongoDbStore.CountAsync(queryable => Order(Filter(queryable, filter), order), cancellationToken);
        var results = await mongoDbStore.FindManyAsync(queryable => Paginate(Filter(queryable, filter), pageArgs), cancellationToken).ToList();
        return new Page<WorkflowExecutionLogRecord>(results, count);
    }

    /// <inheritdoc />
    public async Task<long> DeleteManyAsync(WorkflowExecutionLogRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return await mongoDbStore.DeleteWhereAsync<string>(queryable => Filter(queryable, filter), x => x.Id, cancellationToken);
    }

    private IMongoQueryable<WorkflowExecutionLogRecord> Filter(IMongoQueryable<WorkflowExecutionLogRecord> queryable, WorkflowExecutionLogRecordFilter filter) =>
        (filter.Apply(queryable) as IMongoQueryable<WorkflowExecutionLogRecord>)!;

    private IMongoQueryable<WorkflowExecutionLogRecord> Order<TOrderBy>(IMongoQueryable<WorkflowExecutionLogRecord> queryable, WorkflowExecutionLogRecordOrder<TOrderBy> order) =>
        (queryable.OrderBy(order) as IMongoQueryable<WorkflowExecutionLogRecord>)!;

    private IMongoQueryable<WorkflowExecutionLogRecord> Paginate(IMongoQueryable<WorkflowExecutionLogRecord> queryable, PageArgs pageArgs) =>
        (queryable.Paginate(pageArgs) as IMongoQueryable<WorkflowExecutionLogRecord>)!;
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Extensions;
using Elsa.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Elsa.Persistence.MongoDb.Services
{
    public class MongoWorkflowInstanceStore : IWorkflowInstanceStore
    {
        private readonly IMongoCollection<WorkflowInstance> collection;

        public MongoWorkflowInstanceStore(IMongoCollection<WorkflowInstance> collection)
        {
            this.collection = collection;
        }

        public async Task SaveAsync(WorkflowInstance instance, CancellationToken cancellationToken)
        {
            await collection.ReplaceOneAsync(
                x => x.Id == instance.Id,
                instance,
                new UpdateOptions { IsUpsert = true },
                cancellationToken
            );
        }

        public async Task<WorkflowInstance> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await collection.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<WorkflowInstance> GetByCorrelationIdAsync(string correlationId,
            CancellationToken cancellationToken = default)
        {
            return await collection.AsQueryable()
                .Where(x => x.CorrelationId == correlationId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkflowInstance>> ListByDefinitionAsync(
            string definitionId,
            CancellationToken cancellationToken)
        {
            return await collection.AsQueryable()
                .Where(x => x.DefinitionId == definitionId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkflowInstance>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await collection.AsQueryable().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<(WorkflowInstance, ActivityInstance)>> ListByBlockingActivityAsync(
            string activityType,
            string correlationId = default,
            CancellationToken cancellationToken = default)
        {
            var query = collection.AsQueryable();

            if (!string.IsNullOrWhiteSpace(correlationId))
                query = query.Where(x => x.CorrelationId == correlationId);

            query = query.Where(x => x.BlockingActivities.Any(y => y.ActivityType == activityType));

            var instances = await query.ToListAsync(cancellationToken);

            return instances.GetBlockingActivities(activityType);
        }

        public async Task<IEnumerable<WorkflowInstance>> ListByStatusAsync(
            string definitionId,
            WorkflowStatus status,
            CancellationToken cancellationToken)
        {
            return await collection
                .AsQueryable()
                .Where(
                    x => x.DefinitionId == definitionId && x.Status == status
                )
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkflowInstance>> ListByStatusAsync(
            WorkflowStatus status,
            CancellationToken cancellationToken)
        {
            return await collection
                .AsQueryable()
                .Where(x => x.Status == status)
                .ToListAsync(cancellationToken);
        }
    }
}
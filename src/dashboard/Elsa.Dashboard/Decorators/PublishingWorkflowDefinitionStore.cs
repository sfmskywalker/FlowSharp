﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Messages;
using Elsa.Models;
using Elsa.Persistence;
using MediatR;

namespace Elsa.Dashboard.Decorators
{
    public class PublishingWorkflowDefinitionStore : IWorkflowDefinitionStore
    {
        private readonly IWorkflowDefinitionStore decoratedStore;
        private readonly IMediator mediator;

        public PublishingWorkflowDefinitionStore(IWorkflowDefinitionStore decoratedStore, IMediator mediator)
        {
            this.decoratedStore = decoratedStore;
            this.mediator = mediator;
        }
        
        public async Task<WorkflowDefinitionVersion> SaveAsync(WorkflowDefinitionVersion definition, CancellationToken cancellationToken = default)
        {
            var savedDefinition = await decoratedStore.SaveAsync(definition, cancellationToken);
            await mediator.Publish(new WorkflowDefinitionStoreUpdated(), cancellationToken);
            return savedDefinition;
        }

        public async Task AddAsync(WorkflowDefinitionVersion definition, CancellationToken cancellationToken = default)
        {
            await decoratedStore.AddAsync(definition, cancellationToken);
            
        }

        public Task<WorkflowDefinitionVersion> GetByIdAsync(string id, VersionOptions version, CancellationToken cancellationToken = default)
        {
            return decoratedStore.GetByIdAsync(id, version, cancellationToken);
        }

        public Task<IEnumerable<WorkflowDefinitionVersion>> ListAsync(VersionOptions version, CancellationToken cancellationToken = default)
        {
            return decoratedStore.ListAsync(version, cancellationToken);
        }

        public async Task<WorkflowDefinitionVersion> UpdateAsync(WorkflowDefinitionVersion definition, CancellationToken cancellationToken = default)
        {
            var updatedDefinition = await decoratedStore.UpdateAsync(definition, cancellationToken);
            return updatedDefinition;
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var count = await decoratedStore.DeleteAsync(id, cancellationToken);
            return count;
        }
    }
}
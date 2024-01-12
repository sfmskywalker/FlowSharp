using Xunit;
using System.Threading.Tasks;
using Elsa.Core.IntegrationTests.Autofixture;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Elsa.Services;
using Elsa.Core.IntegrationTests.Workflows;
using Elsa.Persistence;
using Elsa.Testing.Shared;
using Elsa.Testing.Shared.Helpers;

namespace Elsa.Core.IntegrationTests.Persistence.InMemory
{
    public class InMemoryStoreIntegrationTests
    {
        [Theory(DisplayName = "A persistable workflow instance with default persistence behaviour should be persisted-to and readable-from an in-memory store after being run"), AutoMoqData]
        public async Task APersistableWorkflowInstanceWithDefaultPersistanceBehaviourShouldBeRoundTrippable([WithPersistableWorkflow] ElsaHostBuilderBuilder hostBuilderBuilder)
        {
            var hostBuilder = hostBuilderBuilder.GetHostBuilder();
            hostBuilder.ConfigureServices((ctx, services) => {
                services.AddHostedService<HostedWorkflowRunner<PersistableWorkflow>>();
            });
            var host = await hostBuilder.StartAsync();
        }

        [Theory(DisplayName = "A persistable-on-suspend workflow instance should be persisted-to and readable-from an in-memory store after being run"), AutoMoqData]
        public async Task APersistableOnSuspendWorkflowInstanceShouldBeRoundTrippable([WithPersistableWorkflow] ElsaHostBuilderBuilder hostBuilderBuilder)
        {
            var hostBuilder = hostBuilderBuilder.GetHostBuilder();
            hostBuilder.ConfigureServices((ctx, services) => {
                services.AddHostedService<HostedWorkflowRunner<PersistableWorkflow.OnSuspend>>();
            });
            var host = await hostBuilder.StartAsync();
        }

        [Theory(DisplayName = "A persistable-on-activity-executed workflow instance should be persisted-to and readable-from an in-memory store after being run"), AutoMoqData]
        public async Task APersistableOnActivityExecutedWorkflowInstanceShouldBeRoundTrippable([WithPersistableWorkflow] ElsaHostBuilderBuilder hostBuilderBuilder)
        {
            var hostBuilder = hostBuilderBuilder.GetHostBuilder();
            hostBuilder.ConfigureServices((ctx, services) => {
                services.AddHostedService<HostedWorkflowRunner<PersistableWorkflow.OnActivityExecuted>>();
            });
            var host = await hostBuilder.StartAsync();
        }

        [Theory(DisplayName = "A persistable-on-workflow-burst workflow instance should be persisted-to and readable-from an in-memory store after being run"), AutoMoqData]
        public async Task APersistableOnWorkflowBurstWorkflowInstanceShouldBeRoundTrippable([WithPersistableWorkflow] ElsaHostBuilderBuilder hostBuilderBuilder)
        {
            var hostBuilder = hostBuilderBuilder.GetHostBuilder();
            hostBuilder.ConfigureServices((ctx, services) => {
                services.AddHostedService<HostedWorkflowRunner<PersistableWorkflow.OnWorkflowBurst>>();
            });
            var host = await hostBuilder.StartAsync();
        }

        class HostedWorkflowRunner<TWorkflow> : IHostedService where TWorkflow : PersistableWorkflow
        {
            private readonly IServiceScopeFactory _scopeFactory;

            public async Task StartAsync(CancellationToken cancellationToken)
            {
                await using var serviceScope = _scopeFactory.CreateAsyncScope();
                var runWorkflowResult = await serviceScope
                    .ServiceProvider
                    .GetRequiredService<IBuildsAndStartsWorkflow>()
                    .BuildAndStartWorkflowAsync<PersistableWorkflow>(cancellationToken: cancellationToken);
                var instance = runWorkflowResult.WorkflowInstance!;
                var retrievedInstance = await serviceScope
                    .ServiceProvider
                    .GetRequiredService<IWorkflowInstanceStore>()
                    .FindByIdAsync(instance.Id, cancellationToken);

                // An instance should totally be retrieved from the store
                Assert.NotNull(retrievedInstance);
                // Because we are in-memory, it should be a reference to the same instance
                Assert.Same(instance, retrievedInstance);
            }

            public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

            public HostedWorkflowRunner(IServiceScopeFactory scopeFactory)
            {
               _scopeFactory = scopeFactory ?? throw new System.ArgumentNullException(nameof(scopeFactory));
            }
        }
    }
}
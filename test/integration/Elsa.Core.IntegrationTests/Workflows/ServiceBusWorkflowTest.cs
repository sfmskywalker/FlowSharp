using Elsa.Builders;
using Elsa.Models;
using Elsa.Testing.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Elsa.Activities.Console;
using Elsa.Activities.AzureServiceBus;
using Elsa.Activities.AzureServiceBus.Services;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus;
using System.Threading;
using Elsa.Runtime;
using Elsa.Activities.AzureServiceBus.StartupTasks;
using Elsa.Services;
using Elsa.Services.Models;
using System.Reflection;
using Elsa.Services.Workflows;
using Elsa.Activities.AzureServiceBus.Bookmarks;

namespace Elsa.Core.IntegrationTests.Workflows
{
    public class ServiceBusWorkflowTest : WorkflowsUnitTestBase
    {

        public static Mock<ITopicMessageSenderFactory> TopicMessageSenderFactory = new();
        public static Mock<ITopicMessageReceiverFactory> TopicMessageReceiverFactory = new();
        public static Mock<ISenderClient> SenderClient = new();
        public static Mock<IReceiverClient> ReceiverClient = new();
        public static Mock<IWorkflowRegistry> WorkflowRegistryMoq = new();
        public static Mock<IWorkflowLaunchpad> WorkflowLaunchpad = new();
        
        private static IWorkflowBlueprint ServiceBusBluePrint ;

        public ServiceBusWorkflowTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper,
                  services => {
                      services
                      .AddSingleton(TopicMessageSenderFactory.Object)
                      .AddSingleton(TopicMessageReceiverFactory.Object)
                      .AddSingleton<IWorkflowLaunchpad,WorkflowLaunchpad>()
                      .AddSingleton<Scoped<IWorkflowLaunchpad>>()
                      .AddSingleton<IServiceBusTopicsStarter, ServiceBusTopicsStarter>()
                      .AddSingleton<IWorkflowRegistry>(WorkflowRegistryMoq.Object)
                      .AddBookmarkProvider<TopicMessageReceivedBookmarkProvider>()
                      .AddStartupTask<StartServiceBusTopics>();                     
                      
                  },
                  options=> {
                      options
                        .AddActivity<SendAzureServiceBusTopicMessage>()
                        .AddActivity<AzureServiceBusTopicMessageReceived>()
                        
                        ;

                  })
        {

            Func<Message, CancellationToken, Task> handler = null;

            SenderClient
                .Setup(x => x.SendAsync(It.IsAny<Microsoft.Azure.ServiceBus.Message>()))
                .Callback<Message>(msg=>
                {
                        //hack needed to avoid issue when getting msg from SB
                        var systemProperties = msg.SystemProperties;

                        // systemProperties.EnqueuedTimeUtc = DateTime.UtcNow.AddMinutes(1);
                        var bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty;
                        var value = DateTime.UtcNow.AddMinutes(1);
                        systemProperties.GetType().InvokeMember("EnqueuedTimeUtc", bindings, Type.DefaultBinder, systemProperties, new object[] { value });
                        // workaround "ThrowIfNotReceived" by setting "SequenceNumber" value
                        systemProperties.GetType().InvokeMember("SequenceNumber", bindings, Type.DefaultBinder, systemProperties, new object[] { 1 });

                        // message.systemProperties = systemProperties;
                        bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty;
                        msg.GetType().InvokeMember("SystemProperties", bindings, Type.DefaultBinder, msg, new object[] { systemProperties });

                        handler!.Invoke(msg, new CancellationToken());

                    })
                .Returns(Task.FromResult(1));

            ReceiverClient
                .Setup(x => x.RegisterMessageHandler(It.IsAny<Func<Message, CancellationToken, Task>>(), It.IsAny<MessageHandlerOptions>()))
                .Callback<Func<Message, CancellationToken, Task>, MessageHandlerOptions>((messageHandler, options) => {
                    handler = messageHandler;
                });

            ReceiverClient
                .Setup(x => x.Path)
                .Returns($"testtopic2/subscriptions/testsub");

            TopicMessageSenderFactory
                .Setup(x => x.GetTopicSenderAsync(It.IsAny<string>(), default))
                .Returns(Task.FromResult(SenderClient.Object));

            TopicMessageReceiverFactory
                .Setup(x => x.GetTopicReceiverAsync(It.IsAny<string>(), It.IsAny<string>(), default))
                .Returns(Task.FromResult(ReceiverClient.Object));

            ServiceBusBluePrint = WorkflowBuilder.Build<ServiceBusWokflow>();
            WorkflowRegistryMoq
                .Setup(x => x.ListActiveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((new List<IWorkflowBlueprint>() { ServiceBusBluePrint }).AsEnumerable()));

        }

        [Fact(DisplayName = "Send Message Bus after Suspend to avoid receiving response before indexing bookmark.")]
        public async Task SendRequestAfterSuspend()
        {
            var runWorkflowResult = await WorkflowStarter.StartWorkflowAsync(ServiceBusBluePrint);
            
            var workflowInstance = runWorkflowResult.WorkflowInstance!;

            Assert.Equal(WorkflowStatus.Finished, workflowInstance.WorkflowStatus);
        }
    }

    public class ServiceBusWokflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
            .WriteLine(ctx =>
            {
                var correlationId = System.Guid.NewGuid().ToString("n");
                ctx.WorkflowInstance.CorrelationId = correlationId;

                return $"Start! - correlationId: {correlationId}";
            })
            .SendTopicMessage("testtopic2", "\"Hello World\"")
            .TopicMessageReceived<string>("testtopic2", "testsub")
            .WriteLine(ctx => "End: " + (string)ctx.Input);
        }
    }
}

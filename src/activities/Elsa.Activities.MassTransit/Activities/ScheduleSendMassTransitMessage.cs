using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.MassTransit.Options;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Elsa.Activities.MassTransit.Activities
{
    [ActivityDefinition(
        Category = "MassTransit",
        DisplayName = "Schedule MassTransit Message",
        Description = "Schedule a message via MassTransit."
    )]
    public class ScheduleSendMassTransitMessage : Activity
    {
        private readonly ISendEndpointProvider sender;
        private readonly IWorkflowExpressionEvaluator evaluator;
        private readonly MessageScheduleOptions options;

        public ScheduleSendMassTransitMessage(ISendEndpointProvider sender, IWorkflowExpressionEvaluator evaluator, IOptions<MessageScheduleOptions> options)
        {
            this.sender = sender;
            this.evaluator = evaluator;
            this.options = options.Value;
        }

        [ActivityProperty(Hint = "The assembly-qualified type name of the message to send.")]
        public Type MessageType
        {
            get
            {
                var typeName = GetState<string>();
                return string.IsNullOrWhiteSpace(typeName) ? null : System.Type.GetType(typeName);
            }
            set => SetState(value.AssemblyQualifiedName);
        }

        [ActivityProperty(Hint = "An expression that evaluates to the message to send.")]
        public WorkflowExpression Message
        {
            get => GetState<WorkflowExpression>();
            set => SetState(value);
        }

        [ActivityProperty(Hint = "The address of a specific endpoint to send the message to.")]
        public Uri EndpointAddress
        {
            get
            {
                var endpointAddress = GetState<string>();
                return string.IsNullOrEmpty(endpointAddress) ? null : new Uri(endpointAddress);
            }
            set => SetState(value.ToString());
        }

        protected override bool OnCanExecute(WorkflowExecutionContext context)
        {
            return MessageType != null && options.SchedulerAddress != null;
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var message = await evaluator.EvaluateAsync(Message, MessageType, context, cancellationToken);

            var endpoint = await sender.GetSendEndpoint(options.SchedulerAddress);

            var scheduledMessage = await endpoint.ScheduleSend(EndpointAddress, DateTime.UtcNow + TimeSpan.FromSeconds(10), message, cancellationToken);

            context.SetLastResult(Output.SetVariable("TokenId", scheduledMessage.TokenId));

            return Done();
        }
    }
}
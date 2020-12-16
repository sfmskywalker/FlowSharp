using System.Threading.Tasks;
using Elsa.Activities.Timers.Services;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Persistence;
using Elsa.Services;
using Elsa.Services.Models;
using NodaTime;

// ReSharper disable once CheckNamespace
namespace Elsa.Activities.Timers
{
    [Trigger(
        Category = "Timers",
        Description = "Triggers periodically based on a specified CRON expression.",
        Outcomes = new[] { OutcomeNames.Done }
    )]
    public class Cron : Activity
    {
        private readonly IClock _clock;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        private readonly IWorkflowScheduler _workflowScheduler;

        public Cron(IWorkflowInstanceStore workflowInstanceStore, IWorkflowScheduler workflowScheduler, IClock clock)
        {
            _clock = clock;
            _workflowInstanceStore = workflowInstanceStore;
            _workflowScheduler = workflowScheduler;
        }

        [ActivityProperty(Hint = "Specify a CRON expression. See https://crontab.guru/ for help.")]
        public string CronExpression { get; set; } = "* * * * *";

        public Instant? ExecuteAt
        {
            get => GetState<Instant?>();
            set => SetState(value);
        }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            if (context.WorkflowExecutionContext.IsFirstPass)
                return Done();

            var cancellationToken = context.CancellationToken;
            var workflowBlueprint = context.WorkflowExecutionContext.WorkflowBlueprint;
            var workflowInstance = context.WorkflowExecutionContext.WorkflowInstance;
            var executeAt = GetNextOccurrence(CronExpression);

            ExecuteAt = executeAt;

            await _workflowInstanceStore.SaveAsync(context.WorkflowExecutionContext.WorkflowInstance, cancellationToken);
            await _workflowScheduler.ScheduleWorkflowAsync(workflowBlueprint, workflowInstance.EntityId, Id, executeAt, cancellationToken);

            return Suspend();
        }

        protected override IActivityExecutionResult OnResume() => Done();

        private Instant GetNextOccurrence(string cronExpression)
        {
            var schedule = new Quartz.CronExpression(cronExpression);
            var now = _clock.GetCurrentInstant();
            return Instant.FromDateTimeOffset(schedule.GetTimeAfter(now.ToDateTimeOffset())!.Value);
        }
    }
}
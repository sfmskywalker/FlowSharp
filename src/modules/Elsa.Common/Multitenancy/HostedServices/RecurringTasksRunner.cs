using Cronos;
using Elsa.Common.RecurringTasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Elsa.Common.Multitenancy.HostedServices;

[UsedImplicitly]
public class RecurringTasksRunner(IServiceScopeFactory serviceScopeFactory, IOptions<RecurringTaskOptions> options, ISystemClock systemClock) : MultitenantBackgroundService(serviceScopeFactory)
{
    private readonly ICollection<ScheduledTimer> _scheduledTimers = new List<ScheduledTimer>();
    
    protected override async Task StartAsync(TenantScope tenantScope, CancellationToken stoppingToken)
    {
        var tasks = tenantScope.ServiceProvider.GetServices<IRecurringTask>().ToList();
        
        foreach (var task in tasks)
        {
            var schedule = options.Value.Schedule.GetScheduleFor(task.GetType());
            var timer = CreateTimer(schedule, async () => await task.ExecuteAsync(stoppingToken));
            _scheduledTimers.Add(timer);
            await task.StartAsync(stoppingToken);
        }
    }
    
    protected override async Task StopAsync(TenantScope tenantScope, CancellationToken stoppingToken)
    {
        foreach (var timer in _scheduledTimers) await timer.DisposeAsync();
        _scheduledTimers.Clear();
        var tasks = tenantScope.ServiceProvider.GetServices<IRecurringTask>();
        foreach (var task in tasks) await task.StopAsync(stoppingToken);
    }

    private ScheduledTimer CreateTimer(Schedule schedule, Func<Task> action)
    {
        if (schedule.Type == IntervalExpressionType.Interval)
        {
            var dueTime = TimeSpan.Parse(schedule.Expression);
            return new ScheduledTimer(action, () => dueTime);
        }
        
        if (schedule.Type == IntervalExpressionType.Cron)
        {
            var cronExpression = CronExpression.Parse(schedule.Expression);
            return new ScheduledTimer(action, () => (cronExpression.GetNextOccurrence(systemClock.UtcNow.DateTime)! - systemClock.UtcNow.DateTime).Value);
        }
        
        throw new Exception("Invalid interval expression type.");
    }
}
using MoodleClone.Application.Services;
using Quartz;

namespace MoodleClone.Application.Jobs;

public class NotifyTeachersJob : IJob
{
    private readonly NotificationService _notificationService;

    public NotifyTeachersJob(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _notificationService.NotifyTeachersOfMissedSubmissionsAsync();
    }
}
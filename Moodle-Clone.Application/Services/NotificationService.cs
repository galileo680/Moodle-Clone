using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Services;

public class NotificationService
{
    private readonly IAssignmentsRepository _assignmentsRepository;
    private readonly IEmailService _emailService;

    public NotificationService(IAssignmentsRepository assignmentsRepository, IEmailService emailService)
    {
        _assignmentsRepository = assignmentsRepository;
        _emailService = emailService;
    }

    public async Task NotifyTeachersOfMissedSubmissionsAsync()
    {
        var assignments = await _assignmentsRepository.GetAssignmentsWithMissedSubmissionsAsync();

        foreach (var assignment in assignments)
        {
            var teacherEmail = assignment.Course.Owner.Email;
            var studentsWithMissingSubmissions = assignment.Course.Students
                .Where(student => !assignment.Submissions.Any(submission => submission.UserId == student.Id))
                .ToList();

            if (studentsWithMissingSubmissions.Any())
            {
                var studentNames = string.Join(", ", studentsWithMissingSubmissions.Select(s => $"{s.Name} {s.Surname}"));
                var message = $"The following students have missed the submission deadline for the assignment '{assignment.Name}': {studentNames}";

                await _emailService.SendEmailAsync(teacherEmail, "Missed Assignment Submissions", message);
            }
        }
    }
}

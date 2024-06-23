using MediatR;

namespace MoodleClone.Application.Courses.Commands.AcceptStudent;

public class AcceptStudentCommand : IRequest
{
    public int CourseId { get; set; }
    public string StudentId { get; set; }
}
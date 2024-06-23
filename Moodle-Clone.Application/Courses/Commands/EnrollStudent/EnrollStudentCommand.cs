using MediatR;

namespace MoodleClone.Application.Courses.Commands.EnrollStudent;

public class EnrollStudentCommand : IRequest
{
    public int CourseId { get; set; }
    //public string StudentId { get; set; }
}

using MediatR;

namespace MoodleClone.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}

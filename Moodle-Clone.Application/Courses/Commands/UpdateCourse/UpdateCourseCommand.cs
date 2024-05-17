using MediatR;

namespace MoodleClone.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}

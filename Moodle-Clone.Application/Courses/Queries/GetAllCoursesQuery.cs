using MediatR;
using MoodleClone.Application.Repositories.Dtos;

namespace MoodleClone.Application.Courses.Queries;

public class GetAllCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
}

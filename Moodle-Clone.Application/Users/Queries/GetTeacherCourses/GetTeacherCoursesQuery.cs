using MediatR;
using MoodleClone.Application.Repositories.Dtos;

namespace MoodleClone.Application.Users.Queries.GetTeacherCourses;

public class GetTeacherCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
}

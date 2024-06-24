using MediatR;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Users.Queries.GetStudentCourses;

public class GetStudentCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
}

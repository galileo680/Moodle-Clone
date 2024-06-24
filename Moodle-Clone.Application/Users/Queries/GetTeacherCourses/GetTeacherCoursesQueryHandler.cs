using AutoMapper;
using MediatR;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Application.Users.Queries.GetStudentCourses;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Users.Queries.GetTeacherCourses;

public class GetTeacherCoursesQueryHandler(ICoursesRepository coursesRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetTeacherCoursesQuery, IEnumerable<CourseDto>>
{
    public async Task<IEnumerable<CourseDto>> Handle(GetTeacherCoursesQuery request, CancellationToken cancellationToken)
    {
        var currentTeacher = userContext.GetCurrentUser();

        var courses = await coursesRepository.GetTeacherCoursesAsync(currentTeacher.Id);

        var coursesDto = mapper.Map<IEnumerable<CourseDto>>(courses);

        return coursesDto;
    }
}

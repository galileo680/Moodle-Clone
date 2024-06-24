using AutoMapper;
using MediatR;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Users.Queries.GetStudentCourses;

public class GetStudentCoursesQueryHandler(ICoursesRepository coursesRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetStudentCoursesQuery, IEnumerable<CourseDto>>
{
    public async Task<IEnumerable<CourseDto>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
    {
        var currentStudent = userContext.GetCurrentUser();

        var courses = await coursesRepository.GetStudentCoursesAsync(currentStudent.Id);

        var coursesDto = mapper.Map<IEnumerable<CourseDto>>(courses);

        return coursesDto;
    }
}

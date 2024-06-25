using MediatR;
using MoodleClone.Application.Courses.Dtos;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;


namespace MoodleClone.Application.Courses.Queries.GetPendingStudents;

public class GetPendingStudentsQueryHandler(ICoursesRepository coursesRepository,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<GetPendingStudentsQuery, List<PendingStudentDto>>
{
    public async Task<List<PendingStudentDto>> Handle(GetPendingStudentsQuery request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException(nameof(course), request.CourseId.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Update))
            throw new ForbidException();

        var pendingStudents = await coursesRepository.GetPendingStudentsAsync(request.CourseId);

        return pendingStudents.Select(cu => new PendingStudentDto
        {
            StudentId = cu.User.Id,
            Name = cu.User.Name,
            Surname = cu.User.Surname,
            Email = cu.User.Email
        }).ToList();
    }
}

using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Courses.Commands.AcceptStudent;

public class AcceptStudentCommandHandler(ICoursesRepository coursesRepository,
    ICourseEnrollmentsRepository courseEnrollmentsRepository,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<AcceptStudentCommand>
{
    public async Task Handle(AcceptStudentCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Update))
            throw new ForbidException();

        await courseEnrollmentsRepository.AcceptStudentAsync(request.CourseId, request.StudentId);
    }
}

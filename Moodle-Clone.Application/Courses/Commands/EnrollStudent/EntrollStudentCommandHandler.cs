using MediatR;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;


namespace MoodleClone.Application.Courses.Commands.EnrollStudent;

public class EntrollStudentCommandHandler(ICoursesRepository coursesRepository,
    ICourseEnrollmentsRepository courseEnrollmentsRepository,
    IUserContext userContext) : IRequestHandler<EnrollStudentCommand>
{
    public async Task Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        var isStudentEnrolled = await coursesRepository.IsStudentEnrolledAsync(request.CourseId, user.Id);
        if (isStudentEnrolled)
        {
            //throw new InvalidOperationException("Student is already enrolled in the course.");
            throw new ForbidException();
        }

        await courseEnrollmentsRepository.EnrollStudentAsync(request.CourseId, user.Id);
    }

}

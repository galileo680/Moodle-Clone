using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler(ICoursesRepository coursesRepository,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<DeleteCourseCommand>
{
    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.Id);
        if (course is null)
            throw new NotFoundException(nameof(course), request.Id.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Delete))
            throw new ForbidException();


        await coursesRepository.Delete(course);
    }
}

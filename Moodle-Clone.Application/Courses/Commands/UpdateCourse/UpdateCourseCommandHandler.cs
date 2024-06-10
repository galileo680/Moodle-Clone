using AutoMapper;
using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Courses.Commands.UpdateCourse;

internal class UpdateCourseCommandHandler(ICoursesRepository coursesRepository,
    IMapper mapper,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<UpdateCourseCommand>
{
    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException(nameof(course), request.CourseId.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, course);

        await coursesRepository.SaveChanges();
    }
}

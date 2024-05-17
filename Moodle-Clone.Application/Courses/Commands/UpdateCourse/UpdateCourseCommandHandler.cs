using AutoMapper;
using MediatR;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Courses.Commands.UpdateCourse;

internal class UpdateCourseCommandHandler(ICoursesRepository coursesRepository,
    IMapper mapper) : IRequestHandler<UpdateCourseCommand>
{
    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException(nameof(course), request.CourseId.ToString());

        mapper.Map(request, course);

        await coursesRepository.SaveChanges();
    }
}

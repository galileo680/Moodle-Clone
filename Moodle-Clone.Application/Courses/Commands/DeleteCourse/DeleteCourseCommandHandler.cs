using MediatR;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler(ICoursesRepository coursesRepository) : IRequestHandler<DeleteCourseCommand>
{
    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.Id);
        if (course is null)
            throw new NotFoundException(nameof(course), request.Id.ToString());


        await coursesRepository.Delete(course);
    }
}

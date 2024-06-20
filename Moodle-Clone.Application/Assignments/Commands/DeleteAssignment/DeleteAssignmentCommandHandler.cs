using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Assignments.Commands.DeleteAssignment;

public class DeleteAssignmentCommandHandler(ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<DeleteAssignmentCommand>
{
    public async Task Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        var assignment = await assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null || assignment.CourseId != request.CourseId)
            throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Delete))
            throw new ForbidException();

        await assignmentsRepository.Delete(assignment);
    }
}

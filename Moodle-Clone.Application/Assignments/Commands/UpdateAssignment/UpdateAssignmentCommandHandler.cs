using AutoMapper;
using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommandHandler(ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    IMapper mapper,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<UpdateAssignmentCommand>
{
    public async Task Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null)
            throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        /*if (!assignment.CourseId.Equals(request.CourseId))
            throw new ValidationException("The assignment does not belong to the specified course.");*/

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, assignment);

        await assignmentsRepository.SaveChanges();
    }
}

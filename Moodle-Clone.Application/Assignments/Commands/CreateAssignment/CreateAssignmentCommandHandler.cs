using AutoMapper;
using MediatR;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommandHandler(ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    IMapper mapper,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<CreateAssignmentCommand, int>
{
    public async Task<int> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Update))
            throw new ForbidException();

        var assignment = mapper.Map<Assignment>(request);

        return await assignmentsRepository.Create(assignment);
    }
}

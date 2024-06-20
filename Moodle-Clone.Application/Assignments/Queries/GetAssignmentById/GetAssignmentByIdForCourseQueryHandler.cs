using AutoMapper;
using MediatR;
using MoodleClone.Application.Assignments.Dtos;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Assignments.Queries.GetAssignmentById;

public class GetAssignmentByIdForCourseQueryHandler(ICoursesRepository coursesRepository,
    IMapper mapper) : IRequestHandler<GetAssignmentByIdForCourseQuery, AssignmentDto>
{
    public async Task<AssignmentDto> Handle(GetAssignmentByIdForCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await coursesRepository.GetByIdAsync(request.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        var assignment = course.Assignments.FirstOrDefault(a => a.AssignmentId == request.AssignmentId);
        if (assignment == null) throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var result = mapper.Map<AssignmentDto>(assignment);
        return result;
    }
}

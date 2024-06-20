using MediatR;
using MoodleClone.Application.Assignments.Dtos;

namespace MoodleClone.Application.Assignments.Queries.GetAssignments;

public class GetAssignmentsForCourseQuery(int courseId) : IRequest<IEnumerable<AssignmentDto>>
{
    public int courseId { get; set; } = courseId;
}

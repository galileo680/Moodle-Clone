using MediatR;
using MoodleClone.Application.Assignments.Dtos;


namespace MoodleClone.Application.Assignments.Queries.GetAssignmentById;

public class GetAssignmentByIdForCourseQuery(int courseId, int assignmentId) : IRequest<AssignmentDto>
{
    public int CourseId { get; set; } = courseId;
    public int AssignmentId { get; set; } = assignmentId;
}

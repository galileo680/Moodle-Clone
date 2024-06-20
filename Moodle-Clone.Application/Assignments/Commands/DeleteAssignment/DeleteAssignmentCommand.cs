
using MediatR;

namespace MoodleClone.Application.Assignments.Commands.DeleteAssignment;

public class DeleteAssignmentCommand(int courseId, int assignmentId) : IRequest
{
    public int CourseId { get; set; } = courseId;
    public int AssignmentId { get; set; } = assignmentId;
}

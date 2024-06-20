using MediatR;

namespace MoodleClone.Application.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommand : IRequest
{
    public int AssignmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CourseId { get; set; }
}

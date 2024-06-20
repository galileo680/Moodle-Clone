using MediatR;

namespace MoodleClone.Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CourseId { get; set; }
}

namespace MoodleClone.Application.Assignments.Dtos;

public class AssignmentDto
{
    public int AssignmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
}

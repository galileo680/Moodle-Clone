namespace MoodleClone.Domain.Entities;

public class Assignment
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Deadline { get; set; }
    public List<Submission> Submissions { get; set; } = new();
    public Course Course { get; set; } = default!;
    public int CourseId { get; set; } = default!;
}

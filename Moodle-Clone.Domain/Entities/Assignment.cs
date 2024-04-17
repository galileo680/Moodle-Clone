namespace MoodleClone.Domain.Entities;

public class Assignment
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly Deadline { get; set; }
    public List<Submission> Submissions { get; set; } = new();
    public Repository Repository { get; set; } = default!;
    public int RepositoryId { get; set; } = default!;
}

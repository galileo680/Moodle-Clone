namespace MoodleClone.Domain.Entities;

public class Submission
{
    public int Id { get; set; }
    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public Assignment Assignment { get; set; } = default!;
    public int AssignmentId { get; set; } = default!;
    public string StudentId { get; set; } = default!;
}

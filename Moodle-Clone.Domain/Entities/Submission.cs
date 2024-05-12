namespace MoodleClone.Domain.Entities;

public class Submission
{
    /*public int Id { get; set; }
    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public Assignment Assignment { get; set; } = default!;
    public int AssignmentId { get; set; } = default!;
    public string StudentId { get; set; } = default!;*/

    public int SubmissionId { get; set; }
    public string FilePath { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
}

namespace MoodleClone.Application.Submissions.Dtos;

public class SubmissionDto
{
    public int SubmissionId { get; set; }
    public string FilePath { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string UserId { get; set; }
    public int AssignmentId { get; set; }
}

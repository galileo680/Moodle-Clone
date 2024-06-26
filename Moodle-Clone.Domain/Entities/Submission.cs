﻿namespace MoodleClone.Domain.Entities;

public class Submission
{

    public int SubmissionId { get; set; }
    public string FilePath { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
}

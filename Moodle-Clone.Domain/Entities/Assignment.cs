﻿namespace MoodleClone.Domain.Entities;

public class Assignment
{
    public int AssignmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public virtual ICollection<Submission> Submissions { get; set; }
}

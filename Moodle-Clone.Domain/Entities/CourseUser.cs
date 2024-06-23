namespace MoodleClone.Domain.Entities;

public class CourseUser
{
    public Guid Id { get; set; }
    public int CourseId { get; set; }
    public string UserId { get; set; }
    public bool Accepted { get; set; }
    public User User { get; set; }
    public Course Course { get; set; }
}

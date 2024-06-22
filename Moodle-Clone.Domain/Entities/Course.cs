namespace MoodleClone.Domain.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public User Owner { get; set; }
    public virtual ICollection<Assignment> Assignments { get; set; }
    public virtual ICollection<User> Students { get; set; }
    public virtual ICollection<User> PendingStudents { get; set; }

}
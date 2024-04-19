namespace MoodleClone.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Assignment> Assignments { get; set; } = new();
    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public List<User> Students { get; set; } = new();
}

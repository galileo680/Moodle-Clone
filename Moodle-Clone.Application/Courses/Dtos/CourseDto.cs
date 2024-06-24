using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Repositories.Dtos;

public class CourseDto
{
    /*public int CourseId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Assignment> Assignments { get; set; } = new();
    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public List<User> Students { get; set; } = new();*/
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public User Owner { get; set; }
    public virtual ICollection<Assignment> Assignments { get; set; }
    public virtual ICollection<User> Students { get; set; }
}

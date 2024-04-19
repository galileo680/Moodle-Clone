using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Repositories.Dtos;

public class RepositoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Assignment> Assignments { get; set; } = new();
    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public List<User> Students { get; set; } = new();
}

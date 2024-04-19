using Microsoft.AspNetCore.Identity;

namespace MoodleClone.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public List<Repository> OwnedRepositories { get; set; } = [];
}

using Microsoft.AspNetCore.Identity;

namespace MoodleClone.Domain.Entities;

public class User : IdentityUser
{
    public List<Repository> OwnedRepositories { get; set; } = [];
}

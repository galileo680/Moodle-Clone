using Microsoft.AspNetCore.Identity;

namespace Moodle_Clone.Domain.Entities;

public class User : IdentityUser
{
    public List<Repository> OwnedRepositories { get; set; } = [];
}

using Microsoft.AspNetCore.Identity;

namespace MoodleClone.Domain.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Course> OwnedCourses { get; set; } = [];
}

using Microsoft.AspNetCore.Identity;

namespace MoodleClone.Domain.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<Course> CoursesOwned { get; set; }

    public ICollection<CourseUser> CourseStudents { get; set; }
    public virtual ICollection<Submission> Submissions { get; set; }
}

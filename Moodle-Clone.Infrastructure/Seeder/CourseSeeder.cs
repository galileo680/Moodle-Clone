using Microsoft.AspNetCore.Identity;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Seeder;

internal class CourseSeeder(MoodleCloneDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : ICourseSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, user.PasswordHash); // Assuming PasswordHash contains the actual password here
                    if (result.Succeeded)
                    {
                        var role = GetRoleForUser(user);
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Courses.Any())
            {
                dbContext.Courses.AddRange(GetCourses());
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Assignments.Any())
            {
                dbContext.Assignments.AddRange(GetAssignments());
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.CourseUsers.Any())
            {
                dbContext.CourseUsers.AddRange(GetCourseUsers());
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        return new List<IdentityRole>
            {
                new IdentityRole(UserRoles.Student) { NormalizedName = UserRoles.Student.ToUpper() },
                new IdentityRole(UserRoles.Teacher) { NormalizedName = UserRoles.Teacher.ToUpper() },
                new IdentityRole(UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpper() }
            };
    }

    private IEnumerable<User> GetUsers()
    {
        return new List<User>
            {
                new User { UserName = "admin@example.com", Email = "admin@example.com", Name = "Admin", Surname = "Admin", CreatedAt = DateTime.UtcNow, PasswordHash = "Admin123!" },
                new User { UserName = "student1@example.com", Email = "student1@example.com", Name = "Student1", Surname = "One", CreatedAt = DateTime.UtcNow, PasswordHash = "Student123!" },
                new User { UserName = "student2@example.com", Email = "student2@example.com", Name = "Student2", Surname = "Two", CreatedAt = DateTime.UtcNow, PasswordHash = "Student123!" },
                new User { UserName = "student3@example.com", Email = "student3@example.com", Name = "Student3", Surname = "Three", CreatedAt = DateTime.UtcNow, PasswordHash = "Student123!" },
                new User { UserName = "teacher1@example.com", Email = "teacher1@example.com", Name = "Teacher1", Surname = "One", CreatedAt = DateTime.UtcNow, PasswordHash = "Teacher123!" },
                new User { UserName = "teacher2@example.com", Email = "teacher2@example.com", Name = "Teacher2", Surname = "Two", CreatedAt = DateTime.UtcNow, PasswordHash = "Teacher123!" }
            };
    }

    private string GetRoleForUser(User user)
    {
        return user.Email switch
        {
            "admin@example.com" => UserRoles.Admin,
            "student1@example.com" => UserRoles.Student,
            "student2@example.com" => UserRoles.Student,
            "student3@example.com" => UserRoles.Student,
            "teacher1@example.com" => UserRoles.Teacher,
            "teacher2@example.com" => UserRoles.Teacher,
            _ => throw new Exception("Unknown user role")
        };
    }

    private IEnumerable<Course> GetCourses()
    {
        return new List<Course>
            {
                new Course { Name = "Course1", Description = "Description for Course 1", OwnerId = dbContext.Users.First(u => u.Email == "teacher1@example.com").Id },
                new Course { Name = "Course2", Description = "Description for Course 2", OwnerId = dbContext.Users.First(u => u.Email == "teacher2@example.com").Id }
            };
    }

    private IEnumerable<Assignment> GetAssignments()
    {
        return new List<Assignment>
            {
                new Assignment { Name = "Assignment1", Description = "Assignment 1 description", CourseId = 1 },
                new Assignment { Name = "Assignment2", Description = "Assignment 2 description", CourseId = 2 }
            };
    }

    private IEnumerable<CourseUser> GetCourseUsers()
    {
        return new List<CourseUser>
            {
                new CourseUser { CourseId = 1, UserId = dbContext.Users.First(u => u.Email == "student3@example.com").Id, Accepted = true }
            };
    }
}

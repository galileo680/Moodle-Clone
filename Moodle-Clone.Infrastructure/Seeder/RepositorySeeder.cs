using Microsoft.AspNetCore.Identity;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Seeder;

internal class RepositorySeeder(MoodleCloneDbContext dbContext) : IRepositorySeeder
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

            /*if (!dbContext.Users.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }*/

            if (!dbContext.Repositories.Any())
            {
                dbContext.Repositories.AddRange(GetRepositories());
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Assignments.Any())
            {
                dbContext.Assignments.AddRange(GetAssignments());
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.Student)
                {
                    NormalizedName = UserRoles.Student.ToUpper()
                },
                new (UserRoles.Teacher)
                {
                    NormalizedName = UserRoles.Teacher.ToUpper()
                }
            ];

        return roles;
    }

    /*private IEnumerable<User> GetUsers()
    {
        return new List<User>
        {
            new User { Name = "Adam", Surname = "Kowalski", Email = "adam@kowalski.com", PasswordHash = "hashed_password1", CreatedAt = DateTime.UtcNow },
            new User { Name = "Jane", Surname = "Smith", Email = "janesmith@example.com", PasswordHash = "hashed_password2", CreatedAt = DateTime.UtcNow }
        };
    }*/

    private IEnumerable<Repository> GetRepositories()
    {
        return new List<Repository>
        {
            new Repository { Name = "Calculus Course", Description = "Repository for the Calculus course", OwnerId = "1" }
        };
    }
    private IEnumerable<Assignment> GetAssignments()
    {
        return new List<Assignment>
        {
            new Assignment { Name = "Integration", Description = "Submit your integration assignment.", Deadline = DateTime.UtcNow.AddDays(30), RepositoryId = 1 }
        };
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moodle_Clone.Domain.Entities;

namespace Moodle_Clone.Infrastructure.Persistence;

internal class MoodleCloneDbContext(DbContextOptions<MoodleCloneDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Repository> Repositories { get; set; }
    internal DbSet<Assignment> Assignments { get; set; }
    internal DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Repository>()
            .HasMany(r => r.Assignments)
            .WithOne()
            .HasForeignKey(a => a.RepositoryId);

        modelBuilder.Entity<Assignment>()
            .HasMany(a => a.Submissions)
            .WithOne()
            .HasForeignKey(s => s.AssignmentId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRepositories)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}

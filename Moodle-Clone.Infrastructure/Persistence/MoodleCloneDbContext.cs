using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Infrastructure.Persistence;

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
            .WithOne(a => a.Repository)
            .HasForeignKey(a => a.RepositoryId);

        modelBuilder.Entity<Assignment>()
            .HasMany(a => a.Submissions)
            .WithOne(s => s.Assignment)
            .HasForeignKey(s => s.AssignmentId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedRepositories)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}

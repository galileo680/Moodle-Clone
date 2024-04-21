using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Infrastructure.Persistence;

internal class MoodleCloneDbContext(DbContextOptions<MoodleCloneDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Course> Courses { get; set; }
    internal DbSet<Assignment> Assignments { get; set; }
    internal DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>()
            .HasMany(r => r.Assignments)
            .WithOne(a => a.Course)
            .HasForeignKey(a => a.CourseId);

        modelBuilder.Entity<Assignment>()
            .HasMany(a => a.Submissions)
            .WithOne(s => s.Assignment)
            .HasForeignKey(s => s.AssignmentId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedCourses)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }
}

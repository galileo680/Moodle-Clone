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
    internal DbSet<CourseUser> CourseUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacje dla Course
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.CoursesOwned)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        /*modelBuilder.Entity<CourseUser>().Navigation(x => x.User).AutoInclude();
        modelBuilder.Entity<CourseUser>().Navigation(x => x.Course).AutoInclude();*/

        // Relacje dla CourseStudents
        modelBuilder.Entity<CourseUser>()
            .HasKey(cs => cs.Id);

        modelBuilder.Entity<CourseUser>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseStudents)
            .HasForeignKey(cs => cs.CourseId);

        modelBuilder.Entity<CourseUser>()
            .HasOne(cs => cs.User)
            .WithMany(u => u.CourseStudents)
            .HasForeignKey(cs => cs.UserId);

        // Relacje dla Assignment
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Course)
            .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacje dla Submission
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.User)
            .WithMany(u => u.Submissions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Submissions)
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

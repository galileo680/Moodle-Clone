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

        /*modelBuilder.Entity<Course>()
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
            .HasForeignKey(r => r.OwnerId);*/

        // Relacje dla Course
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.CoursesOwned)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)
        .WithMany(s => s.CoursesEnrolled)
        .UsingEntity(j => j.ToTable("CourseStudents")); // Nazwa tabeli join

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

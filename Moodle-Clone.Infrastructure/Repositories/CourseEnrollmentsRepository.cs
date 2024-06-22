using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Repositories;

internal class CourseEnrollmentsRepository(MoodleCloneDbContext dbContext) : ICourseEnrollmentsRepository
{

    public async Task EnrollStudentAsync(int courseId, string studentId)
    {
        var course = await dbContext.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (course == null) throw new NotFoundException(nameof(Course), courseId.ToString());

        var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == studentId);
        if (student == null) throw new NotFoundException(nameof(User), studentId);

        course.Students.Add(student);
        await dbContext.SaveChangesAsync();
    }

    public async Task AcceptStudentAsync(int courseId, string studentId)
    {
        var course = await dbContext.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (course == null) throw new NotFoundException(nameof(Course), courseId.ToString());

        var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == studentId);
        if (student == null) throw new NotFoundException(nameof(User), studentId);

        var pendingStudent = course.PendingStudents.FirstOrDefault(s => s.Id == studentId);
        if (pendingStudent != null)
        {
            course.PendingStudents.Remove(pendingStudent);
            course.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }
    }
}
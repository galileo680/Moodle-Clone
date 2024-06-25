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
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null) throw new NotFoundException(nameof(Course), courseId.ToString());

        var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == studentId);
        if (student == null) throw new NotFoundException(nameof(User), studentId);

        var courseStudent = new CourseUser
        {
            CourseId = courseId,
            UserId = studentId,
            Accepted = false
        };

        dbContext.CourseUsers.Add(courseStudent);
        await dbContext.SaveChangesAsync();
    }

    public async Task AcceptStudentAsync(int courseId, string studentId)
    {
        var course = await dbContext.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null) throw new NotFoundException(nameof(Course), courseId.ToString());

        var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == studentId);
        if (student == null) throw new NotFoundException(nameof(User), studentId);

        var courseStudent = dbContext.CourseUsers.FirstOrDefault(s => s.UserId == studentId && s.CourseId == courseId);

        if (courseStudent != null)
        {
            //dbContext.CourseUsers.Remove(courseStudent);
            courseStudent.Accepted = true;
            course.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }
    }

}
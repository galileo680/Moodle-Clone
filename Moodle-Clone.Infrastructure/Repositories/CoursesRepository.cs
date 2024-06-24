﻿using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Repositories;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Repositories;

internal class CoursesRepository(MoodleCloneDbContext dbContext) : ICoursesRepository
{
    public async Task<int> Create(Course entity)
    {
        dbContext.Courses.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Course entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        var repositories = await dbContext.Courses.ToListAsync();
        return repositories;
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        var repository = await dbContext.Courses
            .Include(r => r.Assignments)
            .ThenInclude(a => a.Submissions)
            .FirstOrDefaultAsync(x => x.Id == id);
        return repository;
    }

    public async Task<string?> GetCourseOwnerSurnameByIdAsync(int courseId)
    {
        var course = await dbContext.Courses
            .Include(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        return course?.Owner?.Surname;
    }

    public async Task<bool> IsStudentAcceptedInCourseAsync(string userId, int courseId)
    {
        return await dbContext.CourseUsers
            .AnyAsync(cu => cu.UserId == userId && cu.CourseId == courseId && cu.Accepted);
    }

    public Task SaveChanges()
        => dbContext.SaveChangesAsync();
}

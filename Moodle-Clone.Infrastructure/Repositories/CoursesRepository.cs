using Microsoft.EntityFrameworkCore;
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
        return entity.CourseId;
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
            .FirstOrDefaultAsync(x => x.CourseId == id);
        return repository;
    }

    public Task SaveChanges()
        => dbContext.SaveChangesAsync();
}

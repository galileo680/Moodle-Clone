using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Repositories;
using MoodleClone.Infrastructure.Persistence;


namespace MoodleClone.Infrastructure.Repositories;

internal class AssignmentsRepository(MoodleCloneDbContext dbContext) : IAssignmentsRepository
{
    public async Task<int> Create(Assignment entity)
    {
        dbContext.Assignments.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.AssignmentId;
    }

    public async Task Delete(Assignment entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAllAsync()
    {
        return await dbContext.Assignments
            .Include(a => a.Course)
            .Include(a => a.Submissions)
            .ToListAsync();
    }

    public async Task<Assignment?> GetByIdAsync(int id)
    {
        return await dbContext.Assignments
            .Include(a => a.Course)
            .Include(a => a.Submissions)
            .FirstOrDefaultAsync(a => a.AssignmentId == id);
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}

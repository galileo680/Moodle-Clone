using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Repositories;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Repositories;

internal class SubmissionRepository(MoodleCloneDbContext dbContext) : ISubmissionsRepository
{
    public async Task<int> Create(Submission entity)
    {
        dbContext.Submissions.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.SubmissionId;
    }

    public async Task Delete(Submission entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Submission>> GetAllAsync()
    {
        return await dbContext.Submissions
            .ToListAsync();
    }

    public async Task<Submission?> GetByIdAsync(int id)
    {
        return await dbContext.Submissions
            .FirstOrDefaultAsync(s => s.SubmissionId == id);
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}

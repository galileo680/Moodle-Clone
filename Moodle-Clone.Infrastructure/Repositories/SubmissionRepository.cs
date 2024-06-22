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

    public async Task<Submission> GetStudentSubmissionAsync(int assignmentId, string userId)
    {
        return await dbContext.Submissions
            .FirstOrDefaultAsync(s => s.AssignmentId == assignmentId && s.UserId == userId);
    }

    public async Task<List<Submission>> GetSubmissionsByAssignmentIdAsync(int assignmentId)
    {
        return await dbContext.Submissions
                .Where(s => s.AssignmentId == assignmentId)
                .ToListAsync();
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Repositories;
using MoodleClone.Infrastructure.Persistence;

namespace MoodleClone.Infrastructure.Repositories;

internal class RepositoriesRepository(MoodleCloneDbContext dbContext) : IRepositoriesRepository
{
    public async Task<int> Create(Repository entity)
    {
        dbContext.Repositories.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Repository entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Repository>> GetAllAsync()
    {
        var repositories = await dbContext.Repositories.ToListAsync();
        return repositories;
    }

    public async Task<Repository?> GetByIdAsync(int id)
    {
        var repository = await dbContext.Repositories
            .Include(r => r.Assignments)
            .ThenInclude(a => a.Submissions)
            .FirstOrDefaultAsync(x => x.Id == id);
        return repository;
    }

    public Task SaveChanges()
        => dbContext.SaveChangesAsync();
}

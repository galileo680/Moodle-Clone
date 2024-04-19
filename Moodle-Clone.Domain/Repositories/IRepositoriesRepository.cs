using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Repositories;

public interface IRepositoriesRepository
{
    Task<IEnumerable<Repository>> GetAllAsync();
    Task<Repository?> GetByIdAsync(int id);
    Task<int> Create(Repository entity);
    Task Delete(Repository entity);
    Task SaveChanges();
}

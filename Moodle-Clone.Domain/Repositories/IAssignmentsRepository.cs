using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Repositories;

public interface IAssignmentsRepository
{
    Task<int> Create(Assignment entity);
    Task Delete(Assignment entity);
    Task<IEnumerable<Assignment>> GetAllAsync();
    Task<Assignment?> GetByIdAsync(int id);
    Task<List<Assignment>> GetAssignmentsWithMissedSubmissionsAsync();
    Task SaveChanges();


}

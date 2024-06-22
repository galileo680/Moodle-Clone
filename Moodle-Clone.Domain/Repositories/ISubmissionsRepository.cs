using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Repositories;

public interface ISubmissionsRepository
{
    Task<int> Create(Submission entity);
    Task Delete(Submission entity);
    Task<IEnumerable<Submission>> GetAllAsync();
    Task<Submission?> GetByIdAsync(int id);
    Task<List<Submission>> GetSubmissionsByAssignmentIdAsync(int assignmentId);
    Task<Submission> GetStudentSubmissionAsync(int assignmentId, string userId);
    Task SaveChanges();
}

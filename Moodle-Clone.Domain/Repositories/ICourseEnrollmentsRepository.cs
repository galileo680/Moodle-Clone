namespace MoodleClone.Domain.Repositories;

public interface ICourseEnrollmentsRepository
{
    Task EnrollStudentAsync(int courseId, string studentId);
    Task AcceptStudentAsync(int courseId, string studentId);
}

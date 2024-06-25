using MediatR;
using MoodleClone.Application.Courses.Dtos;

namespace MoodleClone.Application.Courses.Queries.GetPendingStudents;

public class GetPendingStudentsQuery(int courseId) : IRequest<List<PendingStudentDto>>
{
    public int CourseId { get; set; } = courseId;


}

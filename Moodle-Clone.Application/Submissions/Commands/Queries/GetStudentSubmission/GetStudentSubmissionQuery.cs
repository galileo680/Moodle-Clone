using MediatR;
using MoodleClone.Application.Submissions.Dtos;

namespace MoodleClone.Application.Submissions.Commands.Queries.GetStudentSubmission;

public class GetStudentSubmissionQuery : IRequest<SubmissionDto>
{
    public int AssignmentId { get; set; }
}
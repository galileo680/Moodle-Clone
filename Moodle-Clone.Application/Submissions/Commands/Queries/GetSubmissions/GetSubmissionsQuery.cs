using MediatR;
using MoodleClone.Application.Submissions.Dtos;

namespace MoodleClone.Application.Submissions.Commands.Queries.GetSubmissions;

public class GetSubmissionsQuery : IRequest<IEnumerable<SubmissionDto>>
{
    public int AssignmentId { get; set; }
}

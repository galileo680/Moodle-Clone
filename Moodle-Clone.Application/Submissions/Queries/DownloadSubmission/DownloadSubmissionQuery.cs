using MediatR;
using MoodleClone.Application.Submissions.Dtos;

namespace MoodleClone.Application.Submissions.Queries.DownloadSubmission;

public class DownloadSubmissionQuery : IRequest<SubmissionDto>
{
    public int AssignmentId { get; set; }
    public int SubmissionId { get; set; }

}

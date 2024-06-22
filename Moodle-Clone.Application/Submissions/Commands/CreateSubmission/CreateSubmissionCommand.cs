using MediatR;
using Microsoft.AspNetCore.Http;

namespace MoodleClone.Application.Submissions.Commands.CreateSubmission;

public class CreateSubmissionCommand : IRequest<int>
{
    public IFormFile File { get; set; }
    //public DateTime SubmittedAt { get; set; }
    //public string UserId { get; set; }
    public int AssignmentId { get; set; }
}

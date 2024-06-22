using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Submissions.Commands.CreateSubmission;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using System.Security.Claims;

namespace MoodleClone.API.Controllers
{
    [ApiController]
    [Route("api/course/{courseId}/assignments/{assignmentId}/submissions")]
    [Authorize]
    public class SubmissionsController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<IActionResult> CreateSubmission([FromRoute] int courseId, [FromRoute] int assignmentId, CreateSubmissionCommand command)
        {
            Console.WriteLine(courseId);
            Console.WriteLine(assignmentId);

            command.AssignmentId = assignmentId;
            var id = await mediator.Send(command);
            //return CreatedAtAction(nameof(GetSubmissionById), new { id }, null);
            return Created();
        }
    }
}

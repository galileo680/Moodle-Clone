using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Submissions.Commands.CreateSubmission;
using MoodleClone.Application.Submissions.Queries.GetStudentSubmission;
using MoodleClone.Application.Submissions.Queries.GetSubmissions;
using MoodleClone.Domain.Constants;

namespace MoodleClone.API.Controllers
{
    [ApiController]
    [Route("api/course/{courseId}/assignments/{assignmentId}/submissions")]
    [Authorize]
    public class SubmissionsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<IActionResult> GetSubmissions([FromRoute] int assignmentId)
        {
            var query = new GetSubmissionsQuery { AssignmentId = assignmentId };
            var submissions = await mediator.Send(query);
            return Ok(submissions);
        }

        [HttpGet("mine")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<IActionResult> GetMySubmission([FromRoute] int courseId, [FromRoute] int assignmentId)
        {
            var query = new GetStudentSubmissionQuery { AssignmentId = assignmentId };
            var submission = await mediator.Send(query);
            if (submission == null)
                return NotFound();

            return Ok(submission);
        }

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

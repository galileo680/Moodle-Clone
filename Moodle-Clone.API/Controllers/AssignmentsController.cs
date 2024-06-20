using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Assignments.Commands.CreateAssignment;
using MoodleClone.Application.Assignments.Commands.DeleteAssignment;
using MoodleClone.Application.Assignments.Commands.UpdateAssignment;
using MoodleClone.Application.Assignments.Dtos;
using MoodleClone.Application.Assignments.Queries.GetAssignmentById;
using MoodleClone.Application.Assignments.Queries.GetAssignments;
using MoodleClone.Application.Repositories.Commands.CreateCourse;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;

namespace MoodleClone.API.Controllers;

[ApiController]
[Route("api/course/{CourseId}/assignments")]
[Authorize]
public class AssignmentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAllForCourse([FromRoute] int courseId)
    {
        var assignments = await mediator.Send(new GetAssignmentsForCourseQuery(courseId));
        return Ok(assignments);
    }

    [HttpGet("{assignmentId}")]
    public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetByIdForCourse([FromRoute] int courseId, [FromRoute] int assignmentId)
    {
        var assignment = await mediator.Send(new GetAssignmentByIdForCourseQuery(courseId, assignmentId));
        return Ok(assignment);
    }


    [HttpPost]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> CreateAssignment([FromRoute] int courseId, CreateAssignmentCommand command)
    {
        Console.WriteLine(courseId);
        command.CourseId = courseId;


        var id = await mediator.Send(command);
        return Created();
        //return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{assignmentId}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> UpdateAssignment([FromRoute] int courseId, [FromRoute] int assignmentId, UpdateAssignmentCommand command)
    {
        command.CourseId = courseId;
        command.AssignmentId = assignmentId;

        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{assignmentId}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> DeleteAssignment([FromRoute] int courseId, [FromRoute] int assignmentId)
    {
        var command = new DeleteAssignmentCommand(courseId, assignmentId);
        await mediator.Send(command);
        return NoContent();
    }
}

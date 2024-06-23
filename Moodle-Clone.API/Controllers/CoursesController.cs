using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Courses.Commands.AcceptStudent;
using MoodleClone.Application.Courses.Commands.DeleteCourse;
using MoodleClone.Application.Courses.Commands.EnrollStudent;
using MoodleClone.Application.Courses.Commands.UpdateCourse;
using MoodleClone.Application.Courses.Queries;
using MoodleClone.Application.Repositories.Commands.CreateCourse;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Domain.Constants;
using System.Security.Claims;

namespace MoodleClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
    {
        var courses = await mediator.Send(new GetAllCoursesQuery());
        return Ok(courses);
    }

    [HttpDelete("{courseId}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
    {
        await mediator.Send(new DeleteCourseCommand(courseId));

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        int id = await mediator.Send(command);
        return Created();
        //return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{courseId}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> UpdateCourse([FromRoute] int courseId, UpdateCourseCommand command)
    {
        command.CourseId = courseId;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{courseId}/enroll")]
    [Authorize(Roles = UserRoles.Student)]
    public async Task<IActionResult> EnrollInCourse([FromRoute] int courseId)
    {
        //var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new EnrollStudentCommand { CourseId = courseId };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{courseId}/accept")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> AcceptStudent([FromRoute] int courseId, [FromBody] AcceptStudentCommand command)
    {
        if (courseId != command.CourseId)
            return BadRequest("Course ID in route and body do not match.");

        await mediator.Send(command);
        return NoContent();
    }

}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Courses.Commands.DeleteCourse;
using MoodleClone.Application.Courses.Commands.UpdateCourse;
using MoodleClone.Application.Courses.Queries;
using MoodleClone.Application.Repositories.Commands.CreateCourse;
using MoodleClone.Application.Repositories.Dtos;

namespace MoodleClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
    {
        var courses = await mediator.Send(new GetAllCoursesQuery());
        return Ok(courses);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        await mediator.Send(new DeleteCourseCommand(id));

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        int id = await mediator.Send(command);
        return Created();
        //return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] int id, UpdateCourseCommand command)
    {
        command.CourseId = id;
        await mediator.Send(command);

        return NoContent();
    }


}

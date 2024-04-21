using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Repositories.Commands.CreateCourse;

namespace MoodleClone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{


    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        int id = await mediator.Send(command);
        return Created();
        //return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}

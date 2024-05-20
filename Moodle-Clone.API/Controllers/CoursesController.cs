﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Courses.Commands.DeleteCourse;
using MoodleClone.Application.Courses.Commands.UpdateCourse;
using MoodleClone.Application.Courses.Queries;
using MoodleClone.Application.Repositories.Commands.CreateCourse;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Domain.Constants;

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

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        await mediator.Send(new DeleteCourseCommand(id));

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

    [HttpPatch("{id}")]
    [Authorize(Roles = UserRoles.Teacher)]
    public async Task<IActionResult> UpdateCourse([FromRoute] int id, UpdateCourseCommand command)
    {
        command.CourseId = id;
        await mediator.Send(command);

        return NoContent();
    }


}

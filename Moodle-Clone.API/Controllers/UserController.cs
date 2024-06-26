﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Application.Users.Commands.AssignUserRole;
using MoodleClone.Application.Users.Commands.UnassignUserRole;
using MoodleClone.Application.Users.Dtos;
using MoodleClone.Application.Users.Queries.GetAllUsers;
using MoodleClone.Application.Users.Queries.GetStudentCourses;
using MoodleClone.Application.Users.Queries.GetTeacherCourses;
using MoodleClone.Application.Users.Queries.GetUserRolesByEmail;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;

namespace MoodleClone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IMediator mediator,
        UserManager<User> userManager,
        SignInManager<User> signInManager) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PasswordHash = registerDto.Password,
            };
            var result = await userManager.CreateAsync(user, user.PasswordHash!);
            if (result.Succeeded)
                return Ok("Registration made successfully");

            return BadRequest("Error occured");
        }

        [HttpPatch("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("roles")]
        public async Task<IActionResult> GetUserRolesByEmail([FromQuery] string email)
        {
            var query = new GetUserRolesByEmailQuery(email);
            var roles = await mediator.Send(query);
            return Ok(roles);
        }

        [HttpGet("admin/Users")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var users = await mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("student/courses")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetStudentCourses()
        {
            var query = new GetStudentCoursesQuery();
            var courses = await mediator.Send(query);
            return Ok(courses);
        }

        [HttpGet("teacher/courses")]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTeacherCourses()
        {
            var query = new GetTeacherCoursesQuery();
            var courses = await mediator.Send(query);
            return Ok(courses);
        }

    }
}

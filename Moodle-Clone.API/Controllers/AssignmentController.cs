using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoodleClone.API.Controllers;

[ApiController]
[Route("api/course/{CourseId}/assignments")]
[Authorize]
public class AssignmentController(IMediator mediator) : ControllerBase
{

}

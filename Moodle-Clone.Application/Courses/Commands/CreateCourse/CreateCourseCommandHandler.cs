using AutoMapper;
using MediatR;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Interfaces;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Repositories.Commands.CreateCourse;

public class CreateCourseCommandHandler(IMapper mapper,
    ICoursesRepository coursesRepository,
    IUserContext userContext,
    ICourseAuthorizationService courseAuthorizationService) : IRequestHandler<CreateCourseCommand, int>
{
    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var course = mapper.Map<Course>(request);
        course.OwnerId = currentUser.Id;

        if (!courseAuthorizationService.Authorize(course, ResourceOperation.Create))
            throw new ForbidException();

        int id = await coursesRepository.Create(course);
        return id;
    }
}

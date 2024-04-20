using AutoMapper;
using MediatR;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Repositories.Commands;

public class CreateCourseCommandHandler(IMapper mapper,
    ICoursesRepository coursesRepository,
    IUserContext userContext) : IRequestHandler<CreateCourseCommand, int>
{
    public Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

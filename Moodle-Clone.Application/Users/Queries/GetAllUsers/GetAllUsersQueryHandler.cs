using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoodleClone.Application.Users.Dtos;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IMapper mapper,
    UserManager<User> userManager) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var usersDto = mapper.Map<IEnumerable<UserDto>>(users);

        return usersDto;
    }
}

using MediatR;
using MoodleClone.Application.Users.Dtos;


namespace MoodleClone.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}

using MediatR;

namespace MoodleClone.Application.Users.Queries.GetUserRolesByEmail;

public class GetUserRolesByEmailQuery(string email) : IRequest<IList<string>>
{
    public string Email { get; set; } = email;
}

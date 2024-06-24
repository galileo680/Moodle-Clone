using MediatR;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;

namespace MoodleClone.Application.Users.Queries.GetUserRolesByEmail;

public class GetUserRolesByEmailQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUserRolesByEmailQuery, IList<string>>
{
    public async Task<IList<string>> Handle(GetUserRolesByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        var roles = await userManager.GetRolesAsync(user);
        return roles;
    }
}

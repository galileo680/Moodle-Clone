using MediatR;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;


namespace MoodleClone.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(
UserManager<User> userManager,
RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}

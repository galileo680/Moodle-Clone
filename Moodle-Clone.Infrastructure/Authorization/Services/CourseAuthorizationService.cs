using Microsoft.Extensions.Logging;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Interfaces;

namespace MoodleClone.Infrastructure.Authorization.Services;

public class CourseAuthorizationService(IUserContext userContext) : ICourseAuthorizationService
{
    public bool Authorize(Course course, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();


        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == course.OwnerId)
        {
            return true;
        }

        return false;
    }
}

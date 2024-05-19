using MoodleClone.Domain.Constants;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Domain.Interfaces;

public interface ICourseAuthorizationService
{

    bool Authorize(Course course, ResourceOperation resourceOperation);
}

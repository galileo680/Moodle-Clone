using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MoodleClone.Application.Submissions.Dtos;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Submissions.Queries.GetSubmissions;

public class GetSubmissionsQueryHandler(ISubmissionsRepository submissionsRepository,
    ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetSubmissionsQuery, IEnumerable<SubmissionDto>>
{
    public async Task<IEnumerable<SubmissionDto>> Handle(GetSubmissionsQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var assignment = await assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null) throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var course = await coursesRepository.GetByIdAsync(assignment.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), assignment.CourseId.ToString());


        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        if (user == null) throw new NotFoundException(nameof(User), dbUser.Id.ToString());

        if (course.OwnerId != user.Id) throw new ForbidException();

        var submissions = await submissionsRepository.GetSubmissionsByAssignmentIdAsync(request.AssignmentId);

        return mapper.Map<List<SubmissionDto>>(submissions);


    }
}

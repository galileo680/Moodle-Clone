using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Application.Submissions.Dtos;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Submissions.Commands.Queries.GetStudentSubmission;

public class GetStudentSubmissionQueryHandler(ISubmissionsRepository submissionsRepository,
    ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetStudentSubmissionQuery, SubmissionDto>
{
    public async Task<SubmissionDto> Handle(GetStudentSubmissionQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var assignment = await assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null) throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var course = await coursesRepository.GetByIdAsync(assignment.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), assignment.CourseId.ToString());

        var submission = await submissionsRepository.GetStudentSubmissionAsync(request.AssignmentId, user.Id);
        if (submission == null)
            throw new NotFoundException(nameof(Submission), $"{request.AssignmentId}, {user.Id}");

        return mapper.Map<SubmissionDto>(submission);
    }
}

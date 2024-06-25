using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Application.Submissions.Dtos;
using MoodleClone.Application.Submissions.Queries.GetSubmissions;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Submissions.Queries.DownloadSubmission;

public class DownloadSubmissionHandler : IRequestHandler<DownloadSubmissionQuery, SubmissionDto>
{
    private readonly ISubmissionsRepository _submissionsRepository;
    private readonly ICoursesRepository _coursesRepository;
    private readonly IAssignmentsRepository _assignmentsRepository;
    private readonly IUserStore<User> _userStore;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;

    public DownloadSubmissionHandler(ISubmissionsRepository submissionsRepository,
        ICoursesRepository coursesRepository,
        IAssignmentsRepository assignmentsRepository,
        IUserStore<User> userStore,
        IUserContext userContext,
        IMapper mapper)
    {
        _submissionsRepository = submissionsRepository;
        _coursesRepository = coursesRepository;
        _assignmentsRepository = assignmentsRepository;
        _userStore = userStore;
        _userContext = userContext;
        _mapper = mapper;
    }

    public async Task<SubmissionDto> Handle(DownloadSubmissionQuery request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        var assignment = await _assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null) throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var course = await _coursesRepository.GetByIdAsync(assignment.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), assignment.CourseId.ToString());

        var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken);
        if (user == null) throw new NotFoundException(nameof(User), dbUser.Id.ToString());

        if (course.OwnerId != user.Id) throw new ForbidException();

        var submission = await _submissionsRepository.GetByIdAsync(request.SubmissionId);
        if (submission == null) throw new NotFoundException(nameof(Submission), request.SubmissionId.ToString());

        var submissionDto = _mapper.Map<SubmissionDto>(submission);
        submissionDto.FilePath = submissionDto.FilePath.Replace("\\\\", "\\");
        submissionDto.FileName = Path.GetFileName(submissionDto.FilePath);

        return submissionDto;
    }
}
﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Application.Users;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Submissions.Commands.CreateSubmission;

public class CreateSubmissionCommandHandler(ISubmissionsRepository submissionsRepository,
    ICoursesRepository coursesRepository,
    IAssignmentsRepository assignmentsRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<CreateSubmissionCommand, int>
{
    private readonly string submissionStoragePath = Directory.GetCurrentDirectory() + "\\Files";
    public async Task<int> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {

        var user = userContext.GetCurrentUser();

        var assignment = await assignmentsRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null) throw new NotFoundException(nameof(Assignment), request.AssignmentId.ToString());

        var course = await coursesRepository.GetByIdAsync(assignment.CourseId);
        if (course == null) throw new NotFoundException(nameof(Course), assignment.CourseId.ToString());


        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        if (user == null) throw new NotFoundException(nameof(User), dbUser.Id.ToString());

        var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";

        //var directoryPath = Path.Combine(submissionStoragePath, $"{course.Owner.Surname}_{course.Name}", $"{user.Name}_{user.Surname}", $"Assignment_{request.AssignmentId}");
        var directoryPath = Path.Combine(submissionStoragePath, $"Assignment_{assignment.Name}");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var submission = new Submission
        {
            FilePath = filePath,
            SubmittedAt = DateTime.UtcNow,
            UserId = user.Id,
            AssignmentId = request.AssignmentId
        };

        return await submissionsRepository.Create(submission);
    }
}

using AutoMapper;
using MoodleClone.Application.Submissions.Commands.CreateSubmission;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Submissions.Dtos;

public class SubmissionProfile : Profile
{

    public SubmissionProfile()
    {
        CreateMap<CreateSubmissionCommand, Submission>();
        CreateMap<Submission, SubmissionDto>();
    }

}

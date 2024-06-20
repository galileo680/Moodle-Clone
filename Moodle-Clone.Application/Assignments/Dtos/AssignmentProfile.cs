using AutoMapper;
using MoodleClone.Application.Assignments.Commands.CreateAssignment;
using MoodleClone.Application.Assignments.Commands.UpdateAssignment;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Assignments.Dtos;

public class AssignmentProfile : Profile
{

    public AssignmentProfile()
    {
        CreateMap<CreateAssignmentCommand, Assignment>();
        CreateMap<Assignment, AssignmentDto>();
        CreateMap<UpdateAssignmentCommand, Assignment>();
    }
}

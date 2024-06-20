using FluentValidation;

namespace MoodleClone.Application.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(dto => dto.Name)
             .Length(3, 15);

        RuleFor(dto => dto.Description)
            .Length(5, 100);
    }
}

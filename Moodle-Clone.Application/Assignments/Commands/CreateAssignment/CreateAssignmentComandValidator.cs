using FluentValidation;

namespace MoodleClone.Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentComandValidator : AbstractValidator<CreateAssignmentCommand>
{
    public CreateAssignmentComandValidator()
    {
        RuleFor(dto => dto.Name)
             .Length(3, 15);

        RuleFor(dto => dto.Description)
            .Length(5, 100);
    }
}

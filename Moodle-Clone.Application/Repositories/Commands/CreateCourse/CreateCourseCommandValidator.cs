using FluentValidation;

namespace MoodleClone.Application.Repositories.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 15);

            RuleFor(dto => dto.Description)
                .Length(5, 100);
        }

    }
}

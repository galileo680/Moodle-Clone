using FluentValidation;

namespace MoodleClone.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(dto => dto.Name)
                .Length(3, 15);

        RuleFor(dto => dto.Description)
            .Length(5, 100);
    }
}

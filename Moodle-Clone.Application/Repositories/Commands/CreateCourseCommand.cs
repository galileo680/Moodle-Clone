using MediatR;

namespace MoodleClone.Application.Repositories.Commands;

public class CreateCourseCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}

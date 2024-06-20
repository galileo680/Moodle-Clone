using AutoMapper;
using MediatR;
using MoodleClone.Application.Assignments.Dtos;
using MoodleClone.Domain.Entities;
using MoodleClone.Domain.Exceptions;
using MoodleClone.Domain.Repositories;

namespace MoodleClone.Application.Assignments.Queries.GetAssignments
{
    public class GetAssignmentsForCourseQueryHandler(ICoursesRepository coursesRepository,
        IMapper mapper) : IRequestHandler<GetAssignmentsForCourseQuery, IEnumerable<AssignmentDto>>
    {
        public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignmentsForCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await coursesRepository.GetByIdAsync(request.courseId);

            if (course == null) throw new NotFoundException(nameof(Course), request.courseId.ToString());

            var results = mapper.Map<IEnumerable<AssignmentDto>>(course.Assignments);

            return results;
        }
    }
}

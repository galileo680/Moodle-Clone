using AutoMapper;
using MediatR;
using MoodleClone.Application.Repositories.Dtos;
using MoodleClone.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleClone.Application.Courses.Queries;

internal class GetAllCoursesQueryHandler(IMapper mapper,
    ICoursesRepository coursesRepository) : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
{
    public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await coursesRepository.GetAllAsync();
        var restaurantsDto = mapper.Map<IEnumerable<CourseDto>>(courses);

        return restaurantsDto;
    }
}

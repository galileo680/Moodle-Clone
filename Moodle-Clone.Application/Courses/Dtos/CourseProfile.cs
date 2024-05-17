using AutoMapper;
using MoodleClone.Application.Courses.Commands.UpdateCourse;
using MoodleClone.Application.Repositories.Commands.CreateCourse;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Repositories.Dtos;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<UpdateCourseCommand, Course>();

        CreateMap<Course, CourseDto>();
    }
}

using AutoMapper;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Application.Users.Dtos;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}

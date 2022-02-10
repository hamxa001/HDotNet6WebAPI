using AutoMapper;
using HWebAPI.DTOs.UserDTOs;
using HWebAPI.Models;

namespace HWebAPI
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<UserModel, AddUserDto>()
            .ForMember(x => x.UserName, c => c.MapFrom(x => x.UserName))
            .ForMember(x => x.NormalizedUserName, c => c.MapFrom(x => x.NormalizedUserName))
            .ForMember(x => x.Email, c => c.MapFrom(x => x.Email))
            .ForMember(x => x.NormalizedEmail, c => c.MapFrom(x => x.NormalizedEmail))
            .ForMember(x => x.PasswordHash, c => c.MapFrom(x => x.PasswordHash))
            .ForMember(x => x.PhoneNumber, c => c.MapFrom(x => x.PhoneNumber)).ReverseMap();
        }
    }
}

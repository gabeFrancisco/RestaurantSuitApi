using AutoMapper;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
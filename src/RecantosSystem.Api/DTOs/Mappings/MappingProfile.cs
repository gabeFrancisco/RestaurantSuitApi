using AutoMapper;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();
        }
    }
}
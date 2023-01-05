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
            CreateMap<WorkGroup, WorkGroupDTO>().ReverseMap();

            CreateMap<Event, EventDTO>()
                .ForMember(x => x.Date, y => y.MapFrom(e => e.EventDate.Date))
                .ForMember(x => x.Hour, y => y.MapFrom(e => e.EventDate.Hour))
                .ForMember(x => x.Minutes, y => y.MapFrom(e => e.EventDate.Minute))
                .ReverseMap();
        }
    }
}
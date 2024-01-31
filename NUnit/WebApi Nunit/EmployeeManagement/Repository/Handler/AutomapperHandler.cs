using AutoMapper;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Repository.Handler
{
    public class AutomapperHandler : Profile 
    {
        public AutomapperHandler()
        {
            //CreateMap<EmployeeDto, Employee>()
            //    .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
            //    .AfterMap((src, dest) => dest.Address.EmployeeId = src.EmployeeId);

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<EmployeeRole, EmployeeRoleDto>().ReverseMap();

            CreateMap<Roles, RoleDto>().ReverseMap();

            CreateMap<AddressDto, Address>().ReverseMap();

        }
    }
}

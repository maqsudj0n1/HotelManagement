using Application.DTOs.Customer;
using Application.DTOs.Employee;
using Application.DTOs.Payment;
using Application.DTOs.Permission;
using Application.DTOs.Role;
using Application.DTOs.Room;
using Application.DTOs.RoomClass;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.IdentityEntities;

namespace Application.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            Permission();
            Role();
            Employee();
            Customer();
            Payment();    
            RoomClass();
            Room();
            void Permission()
            {
                CreateMap<Permission, PermissionGetDTO>()
                  .ForMember(x => x.PermissionName, t => t.MapFrom(f => f.PermissionName))
                  .ForMember(x => x.PermissionId, t => t.MapFrom(s => s.Id));
            }

            void Role()
            {
                CreateMap<RoleUpdateDTO, Role>()
                    .ForMember(x => x.Name, t => t.MapFrom(s => s.Name))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                    .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(id => new Permission { Id = id }).ToList()));


                CreateMap<Role, RoleGetDTO>()
                   .ForMember(x => x.Name, t => t.MapFrom(s => s.Name))
                   .ForMember(x => x.RoleId, t => t.MapFrom(d => d.Id))
                   .ForMember(x => x.Permissions, t => t.MapFrom(x => x.Permissions.Select(s => s.Id).ToList()));

                CreateMap<RoleCreateDTO, Role>()
                    .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(id => new Permission() { Id = id }).ToList()))
                    .ForMember(d => d.Name, o => o.MapFrom(src => src.Name));

            }
            void Employee()
            {
                CreateMap<EmployeeCreateDto, Employee>()
                    .ForMember(dest => dest.Roles,
                               opt => opt.MapFrom(scr => scr.Roles.Select(id => new Role() { Id  = id }).ToList()));

                CreateMap<Employee, EmployeeCreateDto>()
                    .ForMember(x=> x.Roles, t=> t.MapFrom(x=> x.Roles.Select(s=> s.Id).ToList()));

                CreateMap<EmployeeGetDto, Employee>()
                    .ForMember(dest => dest.Roles,
                               opt => opt.MapFrom(scr => scr.Roles.Select(id => new Role() { Id  = id }).ToList()));

                CreateMap<Employee, EmployeeGetDto>()
                    .ForMember(x => x.Roles, t => t.MapFrom(x => x.Roles.Select(s => s.Id).ToList()));
            }
            void Customer()
            {
                CreateMap<Customer, CustomerCreateDto>().ReverseMap();
                CreateMap<Customer, CustomerGetDto>().ReverseMap();
                CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
                    
            }
            void Payment()
            {
                CreateMap<Payment, PaymentCreateDto>().ReverseMap();
                CreateMap<Payment, PaymentGetDto>().ReverseMap();
            }
            void RoomClass()
            {
                CreateMap<RoomClass, RoomClassCreateDto>().ReverseMap();
                CreateMap<RoomClass, RoomClassGetDto>().ReverseMap();
            }
            void Room()
            {
                CreateMap<Room, RoomCreateDto>().ReverseMap();
                CreateMap<Room, RoomGetDto>().ReverseMap();
            }

        }
    }
}

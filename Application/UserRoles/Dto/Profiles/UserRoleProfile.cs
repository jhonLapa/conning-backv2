using Application.UserRoles.Dto;
using Application.Usuarios.Dto;
using AutoMapper;

namespace Application.UserRoles.Dtos.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            // UserRol
            CreateMap<Domain.UserRole, UserRoleDto>().ReverseMap();
            CreateMap<Domain.UserRole, UserRoleSaveDto>().ReverseMap();
            CreateMap<Domain.UserRole, UserRoleSelectDto>().ReverseMap();
        }
    }
}

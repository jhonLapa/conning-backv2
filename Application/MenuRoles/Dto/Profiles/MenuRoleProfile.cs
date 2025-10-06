using Application.MenuRoles.Dto;
using Application.Usuarios.Dto;
using AutoMapper;

namespace Application.MenuRoles.Dtos.Profiles
{
    public class MenuRoleProfile : Profile
    {
        public MenuRoleProfile()
        {
            // MenuRole
            CreateMap<Domain.MenuRole, MenuRoleDto>().ReverseMap();
            CreateMap<Domain.MenuRole, MenuRoleSaveDto>().ReverseMap();
            CreateMap<Domain.MenuRole, MenuRoleSelectDto>().ReverseMap();
        }
    }
}

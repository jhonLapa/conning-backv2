using Application.Permissions.Dto;
using AutoMapper;
using Domain;

namespace Application.Permissions.Dtos.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            // Permission básica
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionSaveDto>().ReverseMap();
            CreateMap<Permission, PermissionSelectDto>().ReverseMap();

        }
    }
}

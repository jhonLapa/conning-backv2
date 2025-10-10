using Application.RolePermissions.Dto;
using AutoMapper;

namespace Application.RolePermissions.Dtos.Profiles
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            // UserRol
            CreateMap<Domain.RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<Domain.RolePermission, RolePermissionSaveDto>().ReverseMap();
            CreateMap<RolePermissionSaveDto, Domain.RolePermission>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.PermissionId, opt => opt.Ignore());
        }
    }
}

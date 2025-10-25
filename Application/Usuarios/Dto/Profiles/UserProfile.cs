using Application.Auth.Dto;
using AutoMapper;
using Domain;

namespace Application.Usuarios.Dto.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {

            CreateMap<UserRoleSaveDto, User>();
            CreateMap<UserRoleSaveDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<User , UserDto>().ReverseMap();
            CreateMap<User , UserSaveDto>().ReverseMap();
            CreateMap<User , UserView>().ReverseMap();
            CreateMap<Rol , RolView>().ReverseMap();
            CreateMap<User, UserSelectDto>();
        }
    }
}

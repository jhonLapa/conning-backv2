using Application.Auth.Dto;
using AutoMapper;
using Domain;

namespace Application.Usuarios.Dto.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() { 
        
            CreateMap<User , UserDto>().ReverseMap();
            CreateMap<User , UserSaveDto>().ReverseMap();
            CreateMap<User , UserView>().ReverseMap();
        }
    }
}

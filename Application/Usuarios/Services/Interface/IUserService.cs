using Application.Auth.Dto;
using Application.Core.Services.Interfaces;
using Application.Usuarios.Dto;
using Domain;

namespace Application.Usuarios.Services.Interface
{
    public interface IUserService : ICrudCoreService<UserDto, UserRolSaveDto, int>
    {
        Task<LoginDto> LoginAsync(LoginRequest userAuthDto);
    }
}

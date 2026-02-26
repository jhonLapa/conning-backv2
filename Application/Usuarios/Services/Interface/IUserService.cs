using Application.Core.Services.Interfaces;
using Application.Usuarios.Dto;

namespace Application.Usuarios.Services.Interface
{
    public interface IUserService : ICrudCoreService<UserDto, UserRolSaveDto, int>
    {
    }
}

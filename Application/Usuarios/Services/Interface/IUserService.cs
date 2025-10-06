using Application.Auth.Dto;
using Application.Core.Services.Interfaces;
using Application.Usuarios.Dto;
using Application.UserRoles.Dto;
using Domain;

namespace Application.Usuarios.Services.Interface
{
    public interface IUserService : ICrudCoreService<UserDto, UserRoleSaveDto, int>
    {
        Task<OperationResult<LoginDto>> LoginAsync(LoginRequest userAuthDto);
        Task<PaginadoResponse<UserDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<UserSelectDto>> SelectActivo();
    }
}

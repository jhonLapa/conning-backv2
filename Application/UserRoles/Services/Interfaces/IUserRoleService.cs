using Application.Core.Services.Interfaces;
using Application.UserRoles.Dto;
using Application.Usuarios.Dto;
using Domain;

namespace Application.UserRoles.Services.Interfaces
{
    public interface IUserRoleServices : ICrudCoreService<UserRoleDto, UserRoleSaveDto, int>
    {
        Task<IReadOnlyList<UserRoleSelectDto>> SelectActivo();
        Task<PaginadoResponse<UserRoleDto>> BusquedaPaginado(PaginationRequest dto);
        Task<OperationResult<List<UserRoleDto>>> FindByUserIdAsync(int userId);
        Task<OperationResult<List<UserRoleDto>>> FindByRolIdAsync(int rolId);

    }
}
using Application.Core.Services.Interfaces;
using Application.MenuRoles.Dto;
using Application.Usuarios.Dto;
using Domain;

namespace Application.MenuRoles.Services.Interfaces
{
    public interface IMenuRoleServices : ICrudCoreService<MenuRoleDto, MenuRoleSaveDto, int>
    {
        Task<IReadOnlyList<MenuRoleSelectDto>> SelectActivo();
        Task<PaginadoResponse<MenuRoleDto>> BusquedaPaginado(PaginationRequest dto);
        Task<OperationResult<List<MenuRoleDto>>> FindByMenuIdAsync(int menuId);
        Task<OperationResult<List<MenuRoleDto>>> FindByRolIdAsync(int rolId);

    }
}
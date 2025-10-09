using Application.Permissions.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Permissions.Services.Interfaces
{
    public interface IPermissionServices : ICrudCoreService<PermissionDto, PermissionSaveDto, int>
    {
        Task<IReadOnlyList<PermissionSelectDto>> SelectActivo();
        Task<PaginadoResponse<PermissionDto>> BusquedaPaginado(PaginationRequest dto);
        Task<OperationResult<List<PermissionDto>>> FindByMenuIdAsync(int menuId);
    }
}
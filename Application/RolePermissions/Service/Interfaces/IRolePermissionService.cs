using Application.RolePermissions.Dto;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.RolePermissions.Services.Interfaces
{
    public interface IRolePermissionServices
    {
        Task<RolePermissionDto> FindByIdAsync(int roleId, int permissionId);
        Task<OperationResult<RolePermissionDto>> EditAsync(int roleId, int permissionId, RolePermissionSaveDto saveDto);
        Task<OperationResult<RolePermissionDto>> DisabledAsync(int roleId, int permissionId);

        Task<IReadOnlyList<RolePermissionDto>> FindAllAsync();
        Task<OperationResult<RolePermissionDto>> CreateAsync(RolePermissionSaveDto saveDto);

        Task<IReadOnlyList<RolePermissionSelectDto>> SelectActivo();
        Task<PaginadoResponse<RolePermissionDto>> BusquedaPaginado(PaginationRequest dto);
        Task<OperationResult<List<RolePermissionDto>>> FindByRolIdAsync(int rolId);
        Task<OperationResult<List<RolePermissionDto>>> FindByPermissionIdAsync(int permissionId);
    }
}
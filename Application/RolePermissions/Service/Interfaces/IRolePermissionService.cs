using Application.RolePermissions.Dto;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.RolePermissions.Services.Interfaces
{
    // ✅ CAMBIO CLAVE: Ya NO hereda de ICrudCoreService.
    public interface IRolePermissionServices
    {
        // Métodos que usan la clave compuesta (requieren ambos IDs)
        Task<RolePermissionDto> FindByIdAsync(int roleId, int permissionId);
        Task<OperationResult<RolePermissionDto>> EditAsync(int roleId, int permissionId, RolePermissionSaveDto saveDto);
        Task<OperationResult<RolePermissionDto>> DisabledAsync(int roleId, int permissionId);

        // Métodos de CRUD que usan una sola ID o no usan IDs (se mantienen simples)
        Task<IReadOnlyList<RolePermissionDto>> FindAllAsync();
        Task<OperationResult<RolePermissionDto>> CreateAsync(RolePermissionSaveDto saveDto);

        // Métodos específicos (se mantienen igual)
        Task<IReadOnlyList<RolePermissionSelectDto>> SelectActivo();
        Task<PaginadoResponse<RolePermissionDto>> BusquedaPaginado(PaginationRequest dto);
        Task<OperationResult<List<RolePermissionDto>>> FindByRolIdAsync(int rolId);
        Task<OperationResult<List<RolePermissionDto>>> FindByPermissionIdAsync(int permissionId);
    }
}
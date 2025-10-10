using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRolePermissionRepositorio
    {

        Task<RolePermission> FindByIdAsync(int roleId, int permissionId);

        Task SaveAsync(RolePermission entity);

        Task<IReadOnlyList<RolePermission>> FindAllAsync();
        Task<PaginadoResponse<RolePermission>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<RolePermission>> SelectActivo();
        Task<List<RolePermission>> FindByPermissionIdAsync(int permissionId);
        Task<List<RolePermission>> FindByRolIdAsync(int rolId);
        //Metodo para buscar duplicados
        Task AttachUnchangedAsync(int roleId, int permissionId);
    }
}
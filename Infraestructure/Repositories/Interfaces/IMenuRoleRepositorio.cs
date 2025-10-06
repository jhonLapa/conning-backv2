using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMenuRoleRepositorio : ICrudCoreRespository<MenuRole, int>
    {
        Task<PaginadoResponse<MenuRole>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<MenuRole>> SelectActivo();
        Task<List<MenuRole>> FindByMenuIdAsync(int menuId);
        Task<List<MenuRole>> FindByRolIdAsync(int rolId);
    }
}

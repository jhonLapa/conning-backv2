using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPermissionRepositorio : ICrudCoreRespository<Permission, int>
    {
        Task<PaginadoResponse<Permission>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Permission>> SelectActivo();
        Task<List<Permission>> FindByMenuIdAsync(int menuId);

    }
}

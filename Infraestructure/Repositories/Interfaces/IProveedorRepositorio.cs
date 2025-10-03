using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProveedorRepositorio : ICrudCoreRespository<Proveedor, int>
    {
        Task<PaginadoResponse<Proveedor>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Proveedor>> SelectActivo();

    }
}

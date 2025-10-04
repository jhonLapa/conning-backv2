using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICompraRepositorio : ICrudCoreRespository<Compra, int>
    {
        Task<PaginadoResponse<Compra>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Compra>> SelectActivo();
        Task<List<Compra>> FindByProveedorIdAsync(int proveedorId);
    }
}

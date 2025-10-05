using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDetalleCompraRepositorio : ICrudCoreRespository<DetalleCompra, int>
    {
        Task<PaginadoResponse<DetalleCompra>> BusquedaPaginado(PaginationRequest dto);
        Task<List<DetalleCompra>> ObtenerPorCompraAsync(int idCompra);
    }
}

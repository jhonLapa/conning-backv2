using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDetalleVentaRepositorio : ICrudCoreRespository<DetalleVenta, int>
    {
        Task<PaginadoResponse<DetalleVenta>> BusquedaPaginado(PaginationRequest dto);
        Task<List<DetalleVenta>> ObtenerPorVentaAsync(int idVenta);
        Task DeleteByVentaIdAsync(int idVenta);


    }
}
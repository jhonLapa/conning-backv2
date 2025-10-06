using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPagoCompraCreditoRepositorio : ICrudCoreRespository<PagoCompraCredito, int>
    {
        Task<PaginadoResponse<PagoCompraCredito>> BusquedaPaginado(PaginationRequest dto);
        Task<List<PagoCompraCredito>> ObtenerPorCompraAsync(int idCompra);
        Task DeleteByCompraIdAsync(int IdCompra);

    }
}



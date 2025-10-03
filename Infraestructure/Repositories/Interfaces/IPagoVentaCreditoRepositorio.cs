using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPagoVentaCreditoRepositorio : ICrudCoreRespository<PagoVentaCredito, int>
    {
        Task<PaginadoResponse<PagoVentaCredito>> BusquedaPaginado(PaginationRequest dto);
        Task<List<PagoVentaCredito>> ObtenerPorVentaAsync(int idVenta);

    }
}



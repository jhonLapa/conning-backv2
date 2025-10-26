using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IVentaRepositorio : ICrudCoreRespository<Venta, int>
    {
        Task<PaginadoResponse<Venta>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<IReadOnlyList<Venta>> SelectActivo();
        Task<List<Venta>> FindByClienteIdAsync(int clienteId);
        Task<Venta?> FindByNumeroComprobanteAsync(string serie, string numero, int idTipoComprobante, int? excluirId = null);


    }
}

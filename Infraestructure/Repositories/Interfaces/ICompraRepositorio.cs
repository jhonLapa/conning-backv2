using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICompraRepositorio : ICrudCoreRespository<Compra, int>
    {
        Task<PaginadoResponse<Compra>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<IReadOnlyList<Compra>> SelectActivo();
        Task<List<Compra>> FindByProveedorIdAsync(int proveedorId);
        Task<Compra?> FindByNumeroComprobanteAsync(string serie, string numero, int idTipoComprobante, int? excluirId = null);
    }
}

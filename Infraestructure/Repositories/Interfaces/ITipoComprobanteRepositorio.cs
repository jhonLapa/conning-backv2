using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ITipoComprobanteRepositorio : ICrudCoreRespository<TipoComprobante, int>
    {
        Task<PaginadoResponse<TipoComprobante>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TipoComprobante>> SelectActivo();

    }
}

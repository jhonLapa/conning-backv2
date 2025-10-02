using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ITipoDocumentoRepositorio : ICrudCoreRespository<TipoDocumento, int>
    {
        Task<PaginadoResponse<TipoDocumento>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TipoDocumento>> SelectActivo();

    }
}

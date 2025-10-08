using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAportesPlanillaRepositorio : ICrudCoreRespository<AportesPlanilla, int>
    {
        Task<PaginadoResponse<AportesPlanilla>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<AportesPlanilla>> SelectActivo();
    }
}

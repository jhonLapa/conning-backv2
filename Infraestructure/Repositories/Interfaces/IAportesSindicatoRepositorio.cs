using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAportesSindicatoRepositorio : ICrudCoreRespository<AportesSindicato, int>
    {
        Task<PaginadoResponse<AportesSindicato>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<AportesSindicato>> SelectActivo();
        Task<AportesSindicato?> FindByProyectoMesAnioAsync(int idProyecto, string mesAnio);
        Task DeleteByProyectoIdAsync(int idProyecto);
    }
}

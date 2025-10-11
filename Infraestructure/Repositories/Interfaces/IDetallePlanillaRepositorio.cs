using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDetallePlanillaRepositorio : ICrudCoreRespository<DetallePlanilla, int>
    {
        Task<PaginadoResponse<DetallePlanilla>> BusquedaPaginado(PaginationRequest dto);
        Task<List<DetallePlanilla>> ObtenerPorPlanillaAsync(int idPlanilla);
        Task<List<DetallePlanilla>> ObtenerPorTrabajadorProyectoAsync(int idTrabajadorProyecto);
        Task<IReadOnlyList<DetallePlanilla>> SelectActivo();
    }
}
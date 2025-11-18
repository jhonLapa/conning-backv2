using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ITrabajadorRepositorio : ICrudCoreRespository<Trabajador, int>
    {
        Task<PaginadoResponse<Trabajador>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Trabajador>> SelectActivo();
        Task<IReadOnlyList<Trabajador>> SelectByProyecto(int idProyecto);
        Task<PaginadoResponse<Trabajador>> BusquedaPaginadoConPlanilla(
                PaginationRequest dto,
                DateTime? fechaInicio = null,
                DateTime? fechaFin = null);
    }
}

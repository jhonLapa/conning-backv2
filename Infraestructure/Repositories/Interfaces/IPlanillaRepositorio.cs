
using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPlanillaRepositorio : ICrudCoreRespository<Planilla, int>
    {
        Task<PaginadoResponse<Planilla>> BusquedaPaginadoProyectoTrabajador(PaginationRequest dto, int idTrabajador, int idProyecto);
        Task<PaginadoResponse<Planilla>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<Planilla?> FindByPlanillaAndTrabajadorAsync(int idPlanilla, int idTrabajador);
    }
}



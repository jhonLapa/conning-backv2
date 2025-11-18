using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ITrabajadorProyectoRepositorio : ICrudCoreRespository<TrabajadorProyecto, int>
    {
        Task<PaginadoResponse<TrabajadorProyecto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TrabajadorProyecto>> SelectActivo();
        Task<TrabajadorProyecto?> FindByProyectoYTrabajadorAsync(int idProyecto, int idTrabajador);
        Task DeleteByProyectoIdAsync(int idProyecto);
        Task<bool> ExisteEnPlanillaAsync(int idTrabajadorProyecto);
        Task<List<TrabajadorProyecto>> GetByProyectoIdAsync(int idProyecto);

    }
}
using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProyectoEncargadoRepositorio : ICrudCoreRespository<ProyectoEncargado, int>
    {
        Task<PaginadoResponse<ProyectoEncargado>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ProyectoEncargado>> SelectActivo();
        Task<ProyectoEncargado?> FindByProyectoAsync(int idProyecto);
        Task DeleteByProyectoIdAsync(int idProyecto);
        Task<ProyectoEncargado?> FindLastByProyectoAsync(int idProyecto);
    }
}

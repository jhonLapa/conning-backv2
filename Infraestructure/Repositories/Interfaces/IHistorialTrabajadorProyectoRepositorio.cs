using Domain;
using Domain.Entities;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IHistorialTrabajadorProyectoRepositorio : ICrudCoreRespository<HistorialTrabajadorProyecto, int>
    {
        Task<HistorialTrabajadorProyecto?> FindByTrabajadorProyectoYPlanillaAsync(int idTrabajadorProyecto, int idPlanilla);
    }
}

using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProyectoEncargadoRepositorio : ICrudCoreRespository<ProyectoEncargado, int>
    {
        Task<PaginadoResponse<ProyectoEncargado>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ProyectoEncargado>> SelectActivo();
    }
}

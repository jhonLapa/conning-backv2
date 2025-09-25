using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProjectRepositorio : ICrudCoreRespository<Project, int>
    {
        Task<PaginadoResponse<Project>> BusquedaPaginado(PaginationRequest dto);
    }
}

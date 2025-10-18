using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IProyectoRepositorio : ICrudCoreRespository<Proyecto, int>
    {
        Task<PaginadoResponse<Proyecto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Proyecto>> SelectActivo();
    }
}

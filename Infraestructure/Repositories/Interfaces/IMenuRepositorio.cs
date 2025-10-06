using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMenuRepositorio : ICrudCoreRespository<Menu, int>
    {
        Task<PaginadoResponse<Menu>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Menu>> SelectActivo();
    }
}

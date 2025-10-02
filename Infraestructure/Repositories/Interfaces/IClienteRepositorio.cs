using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IClienteRepositorio : ICrudCoreRespository<Cliente, int>
    {
        Task<PaginadoResponse<Cliente>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Cliente>> SelectActivo();

    }
}

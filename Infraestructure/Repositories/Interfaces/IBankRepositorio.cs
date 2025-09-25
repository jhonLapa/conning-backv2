using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IBankRepositorio : ICrudCoreRespository<Bank, int>
    {
        Task<PaginadoResponse<Bank>> BusquedaPaginado(PaginationRequest dto);

    }
}

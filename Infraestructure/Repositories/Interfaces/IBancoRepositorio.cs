using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IBancoRepositorio : ICrudCoreRespository<Banco, int>
    {
        Task<PaginadoResponse<Banco>> BusquedaPaginado(PaginationRequest dto);
    }
}

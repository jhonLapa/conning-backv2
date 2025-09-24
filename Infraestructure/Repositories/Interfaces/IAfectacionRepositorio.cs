using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAfectacionRepositorio : ICrudCoreRespository<Afectacion, int>
    {
        Task<PaginadoResponse<Afectacion>> BusquedaPaginado(PaginationRequest dto);
    }
}

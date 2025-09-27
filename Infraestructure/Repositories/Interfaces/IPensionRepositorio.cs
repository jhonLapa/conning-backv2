using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPensionRepositorio : ICrudCoreRespository<Pension, int>
    {
        Task<PaginadoResponse<Pension>> BusquedaPaginado(PaginationRequest dto);
    }
}

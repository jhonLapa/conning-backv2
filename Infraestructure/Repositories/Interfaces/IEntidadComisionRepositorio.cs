using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IEntidadComisionRepositorio : ICrudCoreRespository<EntidadComision, int>
    {
        Task<PaginadoResponse<EntidadComision>> BusquedaPaginado(PaginationRequest dto);
    }
}
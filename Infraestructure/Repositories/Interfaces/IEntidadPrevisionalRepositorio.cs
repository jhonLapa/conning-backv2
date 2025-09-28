using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IEntidadPrevisionalRepositorio : ICrudCoreRespository<EntidadPrevisional, int>
    {
        Task<PaginadoResponse<EntidadPrevisional>> BusquedaPaginado(PaginationRequest dto);
    }
}
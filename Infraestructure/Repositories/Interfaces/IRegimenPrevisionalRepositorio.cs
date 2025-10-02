using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRegimenPrevisionalRepositorio : ICrudCoreRespository<RegimenPrevisional, int>
    {
        Task<PaginadoResponse<RegimenPrevisional>> BusquedaPaginado(PaginationRequest dto);
    }
}

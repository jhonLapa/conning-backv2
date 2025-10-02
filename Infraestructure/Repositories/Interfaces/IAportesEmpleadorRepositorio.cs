using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAportesEmpleadorRepositorio : ICrudCoreRespository<AportesEmpleador, int>
    {
        Task<PaginadoResponse<AportesEmpleador>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<AportesEmpleador>> SelectActivo();
    }
}

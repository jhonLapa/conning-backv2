using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMovimientoEspecialRepositorio : ICrudCoreRespository<MovimientoEspecial, int>
    {
        Task<PaginadoResponse<MovimientoEspecial>> BusquedaPaginado(PaginationRequest dto);
    }
}

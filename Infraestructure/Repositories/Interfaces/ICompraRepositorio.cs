using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICompraRepositorio : ICrudCoreRespository<Compra, int>
    {
        Task<PaginadoResponse<Compra>> BusquedaPaginado(PaginationRequest dto);
    }
}

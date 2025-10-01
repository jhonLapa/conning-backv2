
using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICategoriaRepositorio : ICrudCoreRespository<Categoria, int>
    {
        Task<PaginadoResponse<Categoria>> BusquedaPaginado(PaginationRequest dto);

    }
}

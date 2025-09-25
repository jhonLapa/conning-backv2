using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IUsuarioRepositorio : ICrudCoreRespository<User, int>
    {
        Task<User> FindByEmailAsync(string email);
        Task<PaginadoResponse<User>> BusquedaPaginado(PaginationRequest dto);

    }
}

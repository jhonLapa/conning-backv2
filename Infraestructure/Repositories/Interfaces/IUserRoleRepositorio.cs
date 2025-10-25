using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IUserRoleRepositorio : ICrudCoreRespository<UserRole, int>
    {
        Task<PaginadoResponse<UserRole>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<UserRole>> SelectActivo();
        Task<List<UserRole>> FindByUserIdAsync(int userId);
        Task<List<UserRole>> FindByRolIdAsync(int rolId);
        Task<UserRole?> FindByIdAsyncUser(int id);
    }
}

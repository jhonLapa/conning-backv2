using System.Linq.Expressions;

namespace Infraestructure.Core.Repositories.Interfaces
{
    public interface ICrudCoreRespository<T, ID>
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<T?> FindByIdAsync(ID id);
        Task<T> SaveAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, int? excludeId = null);
        Task<string> GenerarCodigoAsync(string prefijo, int longitud = 4);
        Task DeleteAsync(int id);
    }
}

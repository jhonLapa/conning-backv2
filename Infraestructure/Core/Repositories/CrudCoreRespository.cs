using Infraestructure.Contexts;
using Infraestructure.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Core.Repositories
{
    public class CrudCoreRespository<T, ID> : ICrudCoreRespository<T, ID> where T : class
    {
        public readonly ApplicationDbContext _context;

        public CrudCoreRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> FindByIdAsync(ID id)
        {
            return await _context.Set<T>().FindAsync(id); ;
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            EntityState state = _context.Entry(entity).State;

            if (state != EntityState.Unchanged) { 
            
                _ = state switch
                {
                    EntityState.Detached => _context.Set<T>().Add(entity),
                    EntityState.Modified => _context.Set<T>().Update(entity),
                };

                await _context.SaveChangesAsync();
            
            
            }
            

            return entity;
        }

    }
}


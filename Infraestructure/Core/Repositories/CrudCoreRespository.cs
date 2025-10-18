using Infraestructure.Contexts;
using Infraestructure.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, int? excludeId = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (excludeId.HasValue)
            {
                var entityType = _context.Model.FindEntityType(typeof(T));
                var keyName = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;

                if (!string.IsNullOrEmpty(keyName))
                    query = query.Where(e => !EF.Property<int>(e, keyName).Equals(excludeId.Value));
            }

            return await query.AnyAsync(predicate);
        }


        public async Task<string> GenerarCodigoAsync(string prefijo, int longitud = 4)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var codigoProp = entityType?.FindProperty("Codigo");

            if (codigoProp == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene una propiedad 'Codigo'.");

            // Buscar el último código existente
            var ultimo = await _context.Set<T>()
                .AsNoTracking()
                .OrderByDescending(e => EF.Property<string>(e, "Codigo"))
                .Select(e => EF.Property<string>(e, "Codigo"))
                .FirstOrDefaultAsync();

            int numero = 1;
            if (!string.IsNullOrEmpty(ultimo) && ultimo.Length > prefijo.Length)
            {
                var parteNumerica = ultimo.Substring(prefijo.Length);
                if (int.TryParse(parteNumerica, out var n))
                    numero = n + 1;
            }

            return $"{prefijo}{numero.ToString($"D{longitud}")}";
        }


    }
}


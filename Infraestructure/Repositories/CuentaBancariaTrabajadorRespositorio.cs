using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CuentaBancariaTrabajadorRespositorio: CrudCoreRespository<CuentaBancariaTrabajador, int> , ICuentaBancariaTrabajadorRepositorio
    {
        private readonly ApplicationDbContext _context;
        public CuentaBancariaTrabajadorRespositorio(ApplicationDbContext context) : base(context)
        { 
            _context = context;   
        }

        public override async Task<IReadOnlyList<CuentaBancariaTrabajador>> FindAllAsync()
        {
            var response = await _context.Set<CuentaBancariaTrabajador>() // 🚨 Usar Set<T>()
                .Include(e => e.Banco)
                .ToListAsync();

            return response;
        }
        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<CuentaBancariaTrabajador>().FindAsync(id);

            if (entityToDelete != null)
            {
                _context.Set<CuentaBancariaTrabajador>().Remove(entityToDelete);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<IReadOnlyList<CuentaBancariaTrabajador>> FindByTrabajadorIdAsync(int idTrabajador)
        {
            // 🚨 CORRECCIÓN: Usar _context.Set<T>() en lugar de la propiedad DbSet
            return await _context.Set<CuentaBancariaTrabajador>()
                                 .Where(c => c.IdTrabajador == idTrabajador)
                                 .ToListAsync();
        }
    }
}

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
            var response = await _context.Set<CuentaBancariaTrabajador>().
                Include(e => e.Banco).
                ToListAsync();

            return response;
        }

        public async Task DeleteByCuentaTrabajadorIdAsync(int id)
        {
            var detalles = await _context.Set<CuentaBancariaTrabajador>()
                                         .Where(d => d.IdTrabajador == id)
                                         .ToListAsync();

            if (detalles.Any())
            {
                _context.Set<CuentaBancariaTrabajador>().RemoveRange(detalles);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CuentaBancariaTrabajador>> GetByTrabajadorIdAsync(int idTrabajador)
        {
            return await _context.Set<CuentaBancariaTrabajador>()
                .Where(x => x.IdTrabajador == idTrabajador)
                .ToListAsync();
        }

    }
}

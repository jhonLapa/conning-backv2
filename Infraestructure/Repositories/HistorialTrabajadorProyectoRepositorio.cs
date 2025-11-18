using Domain;
using Domain.Entities;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class HistorialTrabajadorProyectoRepositorio : CrudCoreRespository<HistorialTrabajadorProyecto, int>, IHistorialTrabajadorProyectoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public HistorialTrabajadorProyectoRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }


        public async Task<HistorialTrabajadorProyecto?> FindByTrabajadorProyectoYPlanillaAsync(int idTrabajadorProyecto, int idPlanilla)
        {
            return await _context.Set<HistorialTrabajadorProyecto>()
                .Include(h => h.DetallesPlanilla) // si tienes navegación inversa
                .FirstOrDefaultAsync(h =>
                    h.IdTrabajadorProyecto == idTrabajadorProyecto &&
                    h.DetallesPlanilla.Any(d => d.IdPlanilla == idPlanilla));
        }

    }
}

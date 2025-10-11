using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DetallePlanillaRespositorio : CrudCoreRespository<DetallePlanilla, int>, IDetallePlanillaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public DetallePlanillaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<DetallePlanilla>> BusquedaPaginado(PaginationRequest dto)
        {
            var context = _context.Set<DetallePlanilla>().AsQueryable();

            // --- Ordenamiento dinámico ---
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var columnsOrder = dto.Sort.Split(".");
                var column = columnsOrder[0];
                var order = columnsOrder.Length > 1 ? columnsOrder[1] : "asc";

                context = column switch
                {
                    "idDetallePlanilla" => order == "desc" ? context.OrderByDescending(p => p.IdDetallePlanilla) : context.OrderBy(p => p.IdDetallePlanilla),
                    "idPlanilla" => order == "desc" ? context.OrderByDescending(p => p.IdPlanilla) : context.OrderBy(p => p.IdPlanilla),
                    "idTrabajadorProyecto" => order == "desc" ? context.OrderByDescending(p => p.IdTrabajadorProyecto) : context.OrderBy(p => p.IdTrabajadorProyecto),
                    _ => context.OrderBy(p => p.IdDetallePlanilla) // default
                };
            }

            // --- Filtros dinámicos ---
            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");
                    if (id_value.Length < 2) continue;

                    var id = id_value[0];
                    var value = id_value[1];

                    switch (id)
                    {
                        case "idPlanilla":
                            if (int.TryParse(value, out int planillaId))
                                context = context.Where(p => p.IdPlanilla == planillaId);
                            break;
                        case "idTrabajadorProyecto":
                            if (int.TryParse(value, out int trabajadorProyectoId))
                                context = context.Where(p => p.IdTrabajadorProyecto == trabajadorProyectoId);
                            break;
                    }
                }
            }

            // --- Paginado ---
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var data = await context.Skip(skip).Take(take).ToListAsync();
            var total = await context.CountAsync();

            var meta = new Meta
            {
                Page = dto.Page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<DetallePlanilla>(data, meta);
        }



        public async Task<List<DetallePlanilla>> ObtenerPorPlanillaAsync(int idPlanilla)
        {
            return await _context.Set<DetallePlanilla>()
                                 .Include(d => d.Planilla)
                                 .Include(d => d.TrabajadorProyecto)
                                 .Where(d => d.IdPlanilla == idPlanilla)
                                 .ToListAsync();
        }

        public async Task<List<DetallePlanilla>> ObtenerPorTrabajadorProyectoAsync(int idTrabajadorProyecto)
        {
            return await _context.Set<DetallePlanilla>()
                                .Include(d => d.Planilla)
                                 .Include(d => d.TrabajadorProyecto)
                                 .Where(d => d.IdTrabajadorProyecto == idTrabajadorProyecto) 
                                 .ToListAsync();
        }

        public async override Task<DetallePlanilla?> FindByIdAsync(int id)
        {
            var response = await _context.Set<DetallePlanilla>()
                                 .Include(x => x.Planilla) 
                                 .Include(x => x.TrabajadorProyecto) 
                                 .FirstOrDefaultAsync(x => x.IdDetallePlanilla == id);

            return response;
        }

        public async override Task<IReadOnlyList<DetallePlanilla>> FindAllAsync()
        {
            return await _context.Set<DetallePlanilla>()
                                .Include(c => c.Planilla)
                                .Include(c => c.TrabajadorProyecto) 
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<IReadOnlyList<DetallePlanilla>> SelectActivo()
        {
            return await _context.Set<DetallePlanilla>()
               .Include(c => c.Planilla)
               .Include(c => c.TrabajadorProyecto)
               .AsNoTracking()
               .ToListAsync();
        }



    }
}
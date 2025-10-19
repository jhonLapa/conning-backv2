using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PlanillaRepositorio : CrudCoreRespository<Planilla, int>, IPlanillaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public PlanillaRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // ==========================================================
        // 🔹 BÚSQUEDA PAGINADA
        // ==========================================================
        public async Task<PaginadoResponse<Planilla>> BusquedaPaginado(PaginationRequest dto)
        {
            var query = _context.Set<Planilla>()
                .Include(p => p.Proyecto)
                .AsQueryable();

            // Ordenamiento dinámico
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var parts = dto.Sort.Split('.', 2);
                var column = parts.ElementAtOrDefault(0) ?? "createAt";
                var order = parts.ElementAtOrDefault(1) ?? "asc";

                query = column switch
                {
                    "idPlanilla" => order == "desc" ? query.OrderByDescending(p => p.IdPlanilla) : query.OrderBy(p => p.IdPlanilla),
                    "idProyecto" => order == "desc" ? query.OrderByDescending(p => p.IdProyecto) : query.OrderBy(p => p.IdProyecto),
                    "mes" => order == "desc" ? query.OrderByDescending(p => p.Mes) : query.OrderBy(p => p.Mes),
                    "anio" => order == "desc" ? query.OrderByDescending(p => p.Anio) : query.OrderBy(p => p.Anio),
                    "estado" => order == "desc" ? query.OrderByDescending(p => p.Estado) : query.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? query.OrderByDescending(p => p.FechaCreacion) : query.OrderBy(p => p.FechaCreacion),
                    _ => query.OrderByDescending(p => p.FechaCreacion)
                };
            }

            // Filtros dinámicos
            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var parts = filter.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2) continue;

                    var key = parts[0];
                    var value = parts[1].Trim();

                    switch (key)
                    {
                        case "status":
                            if (value.Equals("activo", StringComparison.OrdinalIgnoreCase))
                                query = query.Where(p => p.Estado == 1);
                            else if (value.Equals("inactivo", StringComparison.OrdinalIgnoreCase))
                                query = query.Where(p => p.Estado == 0);
                            break;

                        case "idProyecto":
                            if (int.TryParse(value, out int idProyecto))
                                query = query.Where(p => p.IdProyecto == idProyecto);
                            break;

                        case "anio":
                            if (int.TryParse(value, out int anio))
                                query = query.Where(p => p.Anio == anio);
                            break;

                        case "mes":
                            if (int.TryParse(value, out int mes))
                                query = query.Where(p => p.Mes == mes);
                            break;
                    }
                }
            }

            // Paginación
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var total = await query.CountAsync();
            var data = await query.Skip(skip).Take(take).ToListAsync();

            var meta = new Meta
            {
                Page = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<Planilla>(data, meta);
        }

        // ==========================================================
        // 🔹 DETALLE COMPLETO DE UNA PLANILLA
        // ==========================================================
        public async override Task<Planilla?> FindByIdAsync(int id)
        {
            return await _context.Set<Planilla>()
                .AsSplitQuery() // ✅ evita el warning MultipleCollectionIncludeWarning
                .Include(p => p.Proyecto)
                    .ThenInclude(proy => proy.Cliente)
                .Include(p => p.AportesPlanilla)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.TrabajadorProyecto)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Asistencias)
                .FirstOrDefaultAsync(p => p.IdPlanilla == id);
        }

        // ==========================================================
        // 🔹 OBTENER TODAS LAS PLANILLAS
        // ==========================================================
        public async override Task<IReadOnlyList<Planilla>> FindAllAsync()
        {
            return await _context.Set<Planilla>()
                .AsSplitQuery() // ✅ mejora rendimiento y evita duplicados
                .AsNoTracking()
                .Include(x => x.Proyecto)
                    .ThenInclude(p => p.Cliente)
                .Include(x => x.AportesPlanilla)
                .Include(x => x.Detalles)
                .ToListAsync();
        }
    }
}

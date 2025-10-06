


using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PlanillaRespositorio : CrudCoreRespository<Planilla, int>, IPlanillaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PlanillaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<Planilla>> BusquedaPaginado(PaginationRequest dto)
        {
            var context = _context.Set<Planilla>()
                .Include(p => p.Proyecto) // opcional si necesitas mostrar datos del proyecto
                .AsQueryable();

            // ======================
            // 🔹 Ordenamiento dinámico
            // ======================
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var parts = dto.Sort.Split('.');
                var column = parts.Length > 0 ? parts[0] : string.Empty;
                var order = parts.Length > 1 ? parts[1] : "asc";

                context = column switch
                {
                    "idPlanilla" => order == "desc" ? context.OrderByDescending(p => p.IdPlanilla) : context.OrderBy(p => p.IdPlanilla),
                    "idProyecto" => order == "desc" ? context.OrderByDescending(p => p.IdProyecto) : context.OrderBy(p => p.IdProyecto),
                    "mes" => order == "desc" ? context.OrderByDescending(p => p.Mes) : context.OrderBy(p => p.Mes),
                    "anio" => order == "desc" ? context.OrderByDescending(p => p.Anio) : context.OrderBy(p => p.Anio),
                    "estado" => order == "desc" ? context.OrderByDescending(p => p.Estado) : context.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? context.OrderByDescending(p => p.FechaCreacion) : context.OrderBy(p => p.FechaCreacion),
                    _ => context.OrderByDescending(p => p.FechaCreacion) // por defecto
                };
            }

            // ======================
            // 🔹 Filtros dinámicos
            // ======================
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
                                context = context.Where(p => p.Estado == 1);
                            else if (value.Equals("inactivo", StringComparison.OrdinalIgnoreCase))
                                context = context.Where(p => p.Estado == 0);
                            break;

                        case "idProyecto":
                            if (int.TryParse(value, out int idProyecto))
                                context = context.Where(p => p.IdProyecto == idProyecto);
                            break;

                        case "anio":
                            if (int.TryParse(value, out int anio))
                                context = context.Where(p => p.Anio == anio);
                            break;

                        case "mes":
                            if (int.TryParse(value, out int mes))
                                context = context.Where(p => p.Mes == mes);
                            break;
                    }
                }
            }

            // ======================
            // 🔹 Paginación
            // ======================
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var total = await context.CountAsync();
            var data = await context.Skip(skip).Take(take).ToListAsync();

            var meta = new Meta
            {
                Page = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<Planilla>(data, meta);
        }

        public async override Task<Planilla?> FindByIdAsync(int id)
        {
            var response = await _context.Set<Planilla>()
                .Include(x => x.Proyecto)
                    .ThenInclude(p => p.Cliente) // cliente del proyecto
                .FirstOrDefaultAsync(x => x.IdPlanilla == id);

            return response;
        }

        public async override Task<IReadOnlyList<Planilla>> FindAllAsync()
        {
            return await _context.Set<Planilla>()
                .Include(x => x.Proyecto)
                    .ThenInclude(p => p.Cliente)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}


using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class AportesPlanillaRespositorio : CrudCoreRespository<AportesPlanilla, int>, IAportesPlanillaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public AportesPlanillaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<AportesPlanilla>> BusquedaPaginado(PaginationRequest dto)
        {

            var contex = _context.Set<AportesPlanilla>()
              .Include(c => c.Planilla)
              .AsQueryable();


            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.TipoAporte) : contex.OrderBy(p => p.TipoAporte),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.Estado) : contex.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.FechaVencimiento) : contex.OrderBy(p => p.FechaVencimiento),
                };

            }


            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");

                    var id = id_value[0];
                    var value = id_value[1];

                    if (id == "status")
                    {
                        if (value == "activo") contex = contex.Where(p => p.Estado == 1);
                        if (value == "inactivo") contex = contex.Where(p => p.Estado == 0);
                    }
                    else if (id == "name") contex = contex.Where(p => p.TipoAporte.Contains(value));

                }
            }

            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var data = await contex.Skip(skip).Take(take).ToListAsync();
            var total = await contex.CountAsync();

            var meta = new Meta
            {
                Page = dto.Page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };


            PaginadoResponse<AportesPlanilla> response = new(data, meta);

            return response;
        }


        public async Task<IReadOnlyList<AportesPlanilla>> SelectActivo()
        {
            return await _context.Set<AportesPlanilla>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<AportesPlanilla?> FindByIdAsync(int id)
        {
            return await _context.Set<AportesPlanilla>()
                                 .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                 .FirstOrDefaultAsync(x => x.IdAportePlanilla == id);
        }

        public async Task DeleteRangeAsync(int id)
        {
            var detalles = await _context.Set<AportesPlanilla>()
                                         .Where(d => d.IdPlanilla == id)
                                         .ToListAsync();

            if (detalles.Any())
            {
                _context.Set<AportesPlanilla>().RemoveRange(detalles);
                await _context.SaveChangesAsync();
            }
        }


        public async override Task<IReadOnlyList<AportesPlanilla>> FindAllAsync()
        {
            return await _context.Set<AportesPlanilla>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Planilla)
                                 .ToListAsync();
        }

    }
}

using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DetalleVentaRespositorio : CrudCoreRespository<DetalleVenta, int>, IDetalleVentaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public DetalleVentaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<DetalleVenta>> BusquedaPaginado(PaginationRequest dto)
        {
            var context = _context.Set<DetalleVenta>().AsQueryable();

            // --- Ordenamiento dinámico ---
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var columnsOrder = dto.Sort.Split(".");
                var column = columnsOrder[0];
                var order = columnsOrder.Length > 1 ? columnsOrder[1] : "asc";

                context = column switch
                {
                    "idDetalleVenta" => order == "desc" ? context.OrderByDescending(p => p.IdDetalleVenta) : context.OrderBy(p => p.IdDetalleVenta),
                    "idVenta" => order == "desc" ? context.OrderByDescending(p => p.IdVenta) : context.OrderBy(p => p.IdVenta),
                    "descripcion" => order == "desc" ? context.OrderByDescending(p => p.Descripcion) : context.OrderBy(p => p.Descripcion),
                    _ => context.OrderBy(p => p.IdDetalleVenta) // default
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
                        case "descripcion":
                            context = context.Where(p => p.Descripcion.Contains(value));
                            break;
                        case "unidadMedida":
                            context = context.Where(p => p.UnidadMedida.Contains(value));
                            break;
                        case "idVenta":
                            if (int.TryParse(value, out int ventaId))
                                context = context.Where(p => p.IdVenta == ventaId);
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

            return new PaginadoResponse<DetalleVenta>(data, meta);
        }



        public async Task<List<DetalleVenta>> ObtenerPorVentaAsync(int idVenta)
        {
            return await _context.Set<DetalleVenta>()
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.Cliente)          // Traer Cliente
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.TipoComprobante)  // Traer TipoComprobante
                                 .Where(d => d.IdVenta == idVenta)
                                 .ToListAsync();
        }



        public async override Task<DetalleVenta?> FindByIdAsync(int id)
        {
            var response = await _context.Set<DetalleVenta>()
                .Include(x => x.Venta)
                    .ThenInclude(v => v.Cliente)           
                .Include(x => x.Venta)
                    .ThenInclude(v => v.TipoComprobante)    
                .FirstOrDefaultAsync(x => x.IdDetalleVenta == id);

            return response;
        }



        public async override Task<IReadOnlyList<DetalleVenta>> FindAllAsync()
        {
            return await _context.Set<DetalleVenta>()
                .Include(c => c.Venta)
                    .ThenInclude(v => v.Cliente)        
                .Include(c => c.Venta)
                    .ThenInclude(v => v.TipoComprobante)  
                .AsNoTracking()
                .ToListAsync();
        }



    }
}
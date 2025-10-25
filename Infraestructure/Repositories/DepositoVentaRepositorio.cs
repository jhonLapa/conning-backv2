using Domain;
using Domain.Entities;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DepositoVentaRespositorio : CrudCoreRespository<DepositoVenta, int>, IDepositoVentaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public DepositoVentaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<DepositoVenta>> BusquedaPaginado(PaginationRequest dto)
        {
            var context = _context.Set<DepositoVenta>().AsQueryable();

            // --- Ordenamiento dinámico ---
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var columnsOrder = dto.Sort.Split(".");
                var column = columnsOrder[0];
                var order = columnsOrder.Length > 1 ? columnsOrder[1] : "asc";

                context = column switch
                {
                    "idDepositoVenta" => order == "desc" ? context.OrderByDescending(p => p.IdDepositoVenta) : context.OrderBy(p => p.IdDepositoVenta),
                    "idVenta" => order == "desc" ? context.OrderByDescending(p => p.IdVenta) : context.OrderBy(p => p.IdVenta),
                    _ => context.OrderBy(p => p.IdDepositoVenta) // default
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

            return new PaginadoResponse<DepositoVenta>(data, meta);
        }



        public async Task<List<DepositoVenta>> ObtenerPorVentaAsync(int idVenta)
        {
            return await _context.Set<DepositoVenta>()
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.Cliente)          // Traer Cliente
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.TipoComprobante)  // Traer TipoComprobante
                                 .Where(d => d.IdVenta == idVenta)
                                 .ToListAsync();
        }



        public async override Task<DepositoVenta?> FindByIdAsync(int id)
        {
            var response = await _context.Set<DepositoVenta>()
                .Include(x => x.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(x => x.Venta)
                    .ThenInclude(v => v.TipoComprobante)
                .FirstOrDefaultAsync(x => x.IdDepositoVenta == id);

            return response;
        }



        public async override Task<IReadOnlyList<DepositoVenta>> FindAllAsync()
        {
            return await _context.Set<DepositoVenta>()
                .Include(c => c.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(c => c.Venta)
                    .ThenInclude(v => v.TipoComprobante)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteByVentaIdAsync(int idVenta)
        {
            var detalles = await _context.Set<DepositoVenta>()
                                         .Where(d => d.IdVenta == idVenta)
                                         .ToListAsync();

            if (detalles.Any())
            {
                _context.Set<DepositoVenta>().RemoveRange(detalles);
                await _context.SaveChangesAsync();
            }
        }

    }
}
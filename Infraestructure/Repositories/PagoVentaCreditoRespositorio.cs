
using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PagoVentaCreditoRespositorio : CrudCoreRespository<PagoVentaCredito, int>, IPagoVentaCreditoRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PagoVentaCreditoRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<PagoVentaCredito>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<PagoVentaCredito>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "idPagoVentaCredito" => order == "desc" ? contex.OrderByDescending(p => p.IdPagoVentaCredito) : contex.OrderBy(p => p.IdPagoVentaCredito),
                    "idVenta" => order == "desc" ? contex.OrderByDescending(p => p.IdVenta) : contex.OrderBy(p => p.IdVenta),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.FechaCreacion) : contex.OrderBy(p => p.FechaCreacion),
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
                                contex = contex.Where(p => p.IdVenta == ventaId);
                            break;
                    }
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


            PaginadoResponse<PagoVentaCredito> response = new(data, meta);

            return response;
        }


        public async Task<List<PagoVentaCredito>> ObtenerPorVentaAsync(int idVenta)
        {
            return await _context.Set<PagoVentaCredito>()
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.Cliente)          // Traer Cliente
                                 .Include(d => d.Venta)
                                     .ThenInclude(v => v.TipoComprobante)  // Traer TipoComprobante
                                 .Where(d => d.IdVenta == idVenta)
                                 .ToListAsync();
        }



        public async override Task<PagoVentaCredito?> FindByIdAsync(int id)
        {
            var response = await _context.Set<PagoVentaCredito>()
                .Include(x => x.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(x => x.Venta)
                    .ThenInclude(v => v.TipoComprobante)
                .FirstOrDefaultAsync(x => x.IdPagoVentaCredito == id);

            return response;
        }



        public async override Task<IReadOnlyList<PagoVentaCredito>> FindAllAsync()
        {
            return await _context.Set<PagoVentaCredito>()
                .Include(c => c.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(c => c.Venta)
                    .ThenInclude(v => v.TipoComprobante)
                .AsNoTracking()
                .ToListAsync();
        }




    }
}


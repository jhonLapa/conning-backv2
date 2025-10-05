using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PagoCompraCreditoRespositorio : CrudCoreRespository<PagoCompraCredito, int>, IPagoCompraCreditoRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PagoCompraCreditoRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<PagoCompraCredito>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<PagoCompraCredito>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "idPagoCompraCredito" => order == "desc" ? contex.OrderByDescending(p => p.IdPagoCompraCredito) : contex.OrderBy(p => p.IdPagoCompraCredito),
                    "idCompra" => order == "desc" ? contex.OrderByDescending(p => p.IdCompra) : contex.OrderBy(p => p.IdCompra),
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
                        case "idCompra":
                            if (int.TryParse(value, out int compraId))
                                contex = contex.Where(p => p.IdCompra == compraId);
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


            PaginadoResponse<PagoCompraCredito> response = new(data, meta);

            return response;
        }


        public async Task<List<PagoCompraCredito>> ObtenerPorCompraAsync(int idCompra)
        {
            return await _context.Set<PagoCompraCredito>()
                                 .Include(d => d.Compra)
                                     .ThenInclude(v => v.Proveedor)          // Traer Proveedor
                                 .Include(d => d.Compra)
                                     .ThenInclude(v => v.TipoComprobante)  // Traer TipoComprobante
                                 .Where(d => d.IdCompra == idCompra)
                                 .ToListAsync();
        }



        public async override Task<PagoCompraCredito?> FindByIdAsync(int id)
        {
            var response = await _context.Set<PagoCompraCredito>()
                .Include(x => x.Compra)
                    .ThenInclude(v => v.Proveedor)
                .Include(x => x.Compra)
                    .ThenInclude(v => v.TipoComprobante)
                .FirstOrDefaultAsync(x => x.IdPagoCompraCredito == id);

            return response;
        }



        public async override Task<IReadOnlyList<PagoCompraCredito>> FindAllAsync()
        {
            return await _context.Set<PagoCompraCredito>()
                .Include(c => c.Compra)
                    .ThenInclude(v => v.Proveedor)
                .Include(c => c.Compra)
                    .ThenInclude(v => v.TipoComprobante)
                .AsNoTracking()
                .ToListAsync();
        }




    }
}


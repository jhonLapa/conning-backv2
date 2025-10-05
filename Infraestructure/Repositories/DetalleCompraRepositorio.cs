using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DetalleCompraRespositorio : CrudCoreRespository<DetalleCompra, int>, IDetalleCompraRepositorio
    {
        private readonly ApplicationDbContext _context;
        public DetalleCompraRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<DetalleCompra>> BusquedaPaginado(PaginationRequest dto)
        {
            var context = _context.Set<DetalleCompra>().AsQueryable();

            // --- Ordenamiento dinámico ---
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var columnsOrder = dto.Sort.Split(".");
                var column = columnsOrder[0];
                var order = columnsOrder.Length > 1 ? columnsOrder[1] : "asc";

                context = column switch
                {
                    "idDetalleCompra" => order == "desc" ? context.OrderByDescending(p => p.IdDetalleCompra) : context.OrderBy(p => p.IdDetalleCompra),
                    "idCompra" => order == "desc" ? context.OrderByDescending(p => p.IdCompra) : context.OrderBy(p => p.IdCompra),
                    "descripcion" => order == "desc" ? context.OrderByDescending(p => p.Descripcion) : context.OrderBy(p => p.Descripcion),
                    _ => context.OrderBy(p => p.IdDetalleCompra) // default
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
                        case "idCompra":
                            if (int.TryParse(value, out int ventaId))
                                context = context.Where(p => p.IdCompra == ventaId);
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

            return new PaginadoResponse<DetalleCompra>(data, meta);
        }

        public async Task<List<DetalleCompra>> ObtenerPorCompraAsync(int idCompra)
        {
            return await _context.Set<DetalleCompra>()
                                 .Include(d => d.Compra)
                                     .ThenInclude(v => v.Proveedor)          // Traer Proveedor
                                 .Include(d => d.Compra)
                                     .ThenInclude(v => v.TipoComprobante)  // Traer TipoComprobante
                                 .Where(d => d.IdCompra == idCompra)
                                 .ToListAsync();
        }

        public async override Task<DetalleCompra?> FindByIdAsync(int id)
        {
            var response = await _context.Set<DetalleCompra>()
                .Include(x => x.Compra)
                    .ThenInclude(v => v.Proveedor)
                .Include(x => x.Compra)
                    .ThenInclude(v => v.TipoComprobante)
                .FirstOrDefaultAsync(x => x.IdDetalleCompra == id);

            return response;
        }


        public async override Task<IReadOnlyList<DetalleCompra>> FindAllAsync()
        {
            return await _context.Set<DetalleCompra>()
                .Include(c => c.Compra)
                    .ThenInclude(v => v.Proveedor)
                .Include(c => c.Compra)
                    .ThenInclude(v => v.TipoComprobante)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}



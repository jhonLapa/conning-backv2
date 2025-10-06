using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CompraRespositorio : CrudCoreRespository<Compra, int>, ICompraRepositorio
    {
        private readonly ApplicationDbContext _context;
        public CompraRespositorio(ApplicationDbContext context) : base(context) => _context = context;
       
        public async Task<PaginadoResponse<Compra>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<Compra>()
                     .Include(c => c.Proveedor)
                     .Include(c => c.TipoComprobante)
                     .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Serie) : contex.OrderBy(p => p.Serie),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.Estado) : contex.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.FechaCreacion) : contex.OrderBy(p => p.FechaCreacion),
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
                    else if (id == "name") contex = contex.Where(p => p.Serie.Contains(value));

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


            PaginadoResponse<Compra> response = new(data, meta);

            return response;
        }

        public async Task<IReadOnlyList<Compra>> SelectActivo()
        {
            return await _context.Set<Compra>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }


        public async override Task<Compra?> FindByIdAsync(int id)
        {
            return await _context.Set<Compra>()
                                  .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                  .Include(c => c.Proveedor)
                                  .Include(c => c.TipoComprobante)
                                  .Include(c => c.Detalles)
                                  .Include(c => c.PagosCredito)
                                  .FirstOrDefaultAsync(x => x.IdCompra == id);          
        }


        public async override Task<IReadOnlyList<Compra>> FindAllAsync()
        {
            return await _context.Set<Compra>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Proveedor)
                                 .Include(c => c.TipoComprobante)          
                                 .Include(c => c.Detalles)
                                 .Include(c => c.PagosCredito)
                                 .ToListAsync();
        }

        public async Task<List<Compra>> FindByProveedorIdAsync(int proveedorId)
        {
            return await _context.Set<Compra>()
                                 .AsNoTracking()                                
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Proveedor)
                                 .Include(v => v.TipoComprobante)
                                 .Include(v => v.Detalles)
                                 .Include(v => v.PagosCredito)
                                 .Where(v => v.IdProveedor == proveedorId)
                                 .ToListAsync();
        }

        public async Task<Compra?> FindByNumeroComprobanteAsync(string serie, string numero, int idTipoComprobante, int? excluirId = null)
        {
            var query = _context.Set<Compra>()
                .AsNoTracking()
                .Where(c =>
                    c.IdTipoComprobante == idTipoComprobante &&
                    c.Serie.ToLower() == serie.ToLower() &&
                    c.Numero.ToLower() == numero.ToLower());

            if (excluirId.HasValue)
                query = query.Where(c => c.IdCompra != excluirId.Value);

            return await query.FirstOrDefaultAsync();
        }
    }
}

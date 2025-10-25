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
            var context = _context.Set<Compra>()
                .Include(c => c.Proveedor)
                .Include(c => c.TipoComprobante)
                //.Include(c => c.Proyecto)
                .AsQueryable();

            // ============================================================
            // 🔹 ORDENAMIENTO DINÁMICO
            // ============================================================
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var columnsOrder = dto.Sort.Split(".");
                if (columnsOrder.Length == 2)
                {
                    var column = columnsOrder[0];
                    var order = columnsOrder[1].ToLower();

                    context = column switch
                    {
                        "serie" => order == "desc"
                            ? context.OrderByDescending(p => p.Serie)
                            : context.OrderBy(p => p.Serie),

                        "proveedor" => order == "desc"
                            ? context.OrderByDescending(p => p.Proveedor.NombreCompleto)
                            : context.OrderBy(p => p.Proveedor.NombreCompleto),

                        //"proyecto" => order == "desc"
                        //    ? context.OrderByDescending(p => p.Proyecto.Nombre)
                        //    : context.OrderBy(p => p.Proyecto.Nombre),


                        "status" => order == "desc"
                            ? context.OrderByDescending(p => p.Estado)
                            : context.OrderBy(p => p.Estado),

                        "createAt" => order == "desc"
                            ? context.OrderByDescending(p => p.FechaCreacion)
                            : context.OrderBy(p => p.FechaCreacion),

                        _ => context
                    };
                }
            }

            // ============================================================
            // 🔹 FILTROS DINÁMICOS
            // ============================================================
            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(':', 2);
                    if (id_value.Length < 2) continue;

                    var id = id_value[0].Trim().ToLower();
                    var value = id_value[1].Trim();

                    if (string.IsNullOrWhiteSpace(value))
                        continue;

                    var val = value.ToLower().Replace("-", "").Trim(); // limpia guiones

                    switch (id)
                    {
                        // ------------------------------------------------------------
                        // ✅ ESTADO
                        // ------------------------------------------------------------
                        case "status":
                            if (value.Equals("activo", StringComparison.OrdinalIgnoreCase))
                                context = context.Where(p => p.Estado == 1);
                            else if (value.Equals("inactivo", StringComparison.OrdinalIgnoreCase))
                                context = context.Where(p => p.Estado == 0);
                            break;

                        // ------------------------------------------------------------
                        // ✅ NÚMERO COMPROBANTE (serie, número o combinado)
                        // ------------------------------------------------------------
                        case "numerocomprobante":
                            context = context.Where(p =>
                                (p.Serie + p.Numero).ToLower().Contains(val) ||          // F001001
                                (p.Serie + "-" + p.Numero).ToLower().Contains(val) ||    // F001-001
                                p.Serie.ToLower().Contains(val) ||                      // F001
                                p.Numero.ToString().Contains(val)                       // 001
                            );
                            break;

                        // ------------------------------------------------------------
                        // ✅ PROVEEDOR
                        // ------------------------------------------------------------
                        case "proveedor":
                            context = context.Where(p =>
                                p.Proveedor != null && p.Proveedor.NombreCompleto.ToLower().Contains(val));
                            break;

                        // ------------------------------------------------------------
                        // ✅ PROYECTO
                        // ------------------------------------------------------------
                        //case "proyecto":
                        //    context = context.Where(p =>
                        //        p.Proyecto != null && p.Proyecto.Nombre.ToLower().Contains(val));
                        //    break;

                        // ------------------------------------------------------------
                        // ✅ TIPO DE COMPROBANTE
                        // ------------------------------------------------------------
                        case "tipocomprobante":
                            context = context.Where(p =>
                                p.TipoComprobante != null && p.TipoComprobante.Nombre.ToLower().Contains(val));
                            break;
                    }
                }
            }

            // ============================================================
            // 🔹 PAGINACIÓN
            // ============================================================
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var total = await context.CountAsync();
            var data = await context.Skip(skip).Take(take).ToListAsync();

            // ============================================================
            // 🔹 META
            // ============================================================
            var meta = new Meta
            {
                Page = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<Compra>(data, meta);
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


using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class VentaRespositorio : CrudCoreRespository<Venta, int>, IVentaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public VentaRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<Venta>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false)
        {
            var context = _context.Set<Venta>()
                .Include(c => c.Cliente)
                .Include(c => c.TipoComprobante)
                .Include(c => c.Proyecto)
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

                        "cliente" => order == "desc"
                            ? context.OrderByDescending(p => p.Cliente.NombreCompleto)
                            : context.OrderBy(p => p.Cliente.NombreCompleto),

                        "proyecto" => order == "desc"
                            ? context.OrderByDescending(p => p.Proyecto.Nombre)
                            : context.OrderBy(p => p.Proyecto.Nombre),

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

                    var val = value.ToLower().Replace("-", "").Trim();

                    switch (id)
                    {
                        case "status":
                            if (value.Equals("activo", StringComparison.OrdinalIgnoreCase))
                                context = context.Where(p => p.Estado == 1);
                            else if (value.Equals("inactivo", StringComparison.OrdinalIgnoreCase))
                                context = context.Where(p => p.Estado == 0);
                            break;

                        case "numerocomprobante":
                        case "comprobante": // 👈 Por compatibilidad
                            context = context.Where(p =>
                                (p.Serie + p.Numero).ToLower().Contains(val) ||
                                (p.Serie + "-" + p.Numero).ToLower().Contains(val) ||
                                p.Serie.ToLower().Contains(val) ||
                                p.Numero.ToString().Contains(val)
                            );
                            break;

                        case "cliente":
                            context = context.Where(p =>
                                p.Cliente != null && p.Cliente.NombreCompleto.ToLower().Contains(val));
                            break;

                        case "proyecto":
                            context = context.Where(p =>
                                p.Proyecto != null && p.Proyecto.Nombre.ToLower().Contains(val));
                            break;

                        case "tipocomprobante":
                            context = context.Where(p =>
                                p.TipoComprobante != null && p.TipoComprobante.Nombre.ToLower().Contains(val));
                            break;
                    }
                }
            }

            // ============================================================
            // 🔹 PAGINACIÓN (condicional)
            // ============================================================
            List<Venta> data;
            int total;

            if (descargarTodo)
            {
                // 🔸 Descargar todo → sin paginar
                data = await context.ToListAsync();
                total = data.Count;
            }
            else
            {
                var take = dto.Take ?? 5;
                var page = dto.Page ?? 1;
                var skip = (page - 1) * take;

                total = await context.CountAsync();
                data = await context.Skip(skip).Take(take).ToListAsync();
            }

            // ============================================================
            // 🔹 META
            // ============================================================
            var meta = new Meta
            {
                Page = dto.Page ?? 1,
                TotalCount = total,
                TotalPages = descargarTodo
                    ? 1
                    : (int)Math.Ceiling((double)total / (dto.Take ?? 5))
            };

            return new PaginadoResponse<Venta>(data, meta);
        }

        public async Task<IReadOnlyList<Venta>> SelectActivo()
        {
            return await _context.Set<Venta>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<Venta?> FindByIdAsync(int id)
        {
            return await _context.Set<Venta>()
                                 .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                 .Include(c => c.Cliente)
                                 .Include(c => c.TipoComprobante)
                                 .Include(c => c.Detalles)
                                 .Include(c => c.PagosCredito)
                                 .Include(c => c.DepositosVenta)
                                 .Include(c => c.Proyecto)
                                 .FirstOrDefaultAsync(x => x.IdVenta == id);
        }



        public async override Task<IReadOnlyList<Venta>> FindAllAsync()
        {
            return await _context.Set<Venta>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Cliente)
                                 .Include(c => c.TipoComprobante)
                                 .Include(c => c.Detalles)
                                 .Include(c => c.PagosCredito)
                                 .Include(c => c.DepositosVenta)
                                 .Include(c => c.Proyecto)
                                 .ToListAsync();
        }

        public async Task<List<Venta>> FindByClienteIdAsync(int clienteId)
        {
            return await _context.Set<Venta>()
                .AsNoTracking()
                .AsSplitQuery() // evita el warning MultipleCollectionInclude
                .Include(v => v.Cliente)
                .Include(v => v.TipoComprobante)
                .Include(v => v.Detalles)
                .Include(v => v.PagosCredito)
                .Include(c => c.Proyecto)
                .Where(v => v.IdCliente == clienteId)
                .ToListAsync();
        }

        public async Task<Venta?> FindByNumeroComprobanteAsync(string serie, string numero, int idTipoComprobante, int? excluirId = null)
        {
            var query = _context.Set<Venta>()
                .AsNoTracking()
                .Where(v =>
                    v.IdTipoComprobante == idTipoComprobante &&
                    v.Serie.ToLower() == serie.ToLower() &&
                    v.Numero.ToLower() == numero.ToLower());

            if (excluirId.HasValue)
                query = query.Where(v => v.IdVenta != excluirId.Value);

            return await query.FirstOrDefaultAsync();
        }
    }
}
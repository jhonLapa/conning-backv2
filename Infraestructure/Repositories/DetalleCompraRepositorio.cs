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
            var contex = _context.Set<DetalleCompra>()
                     .Include(c => c.Compra)
                     .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Descripcion) : contex.OrderBy(p => p.Descripcion),
                };

            }


            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");

                    var id = id_value[0];
                    var value = id_value[1];
               
                    if (id == "name") contex = contex.Where(p => p.Descripcion.Contains(value));

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


            PaginadoResponse<DetalleCompra> response = new(data, meta);

            return response;
        }


        public async override Task<DetalleCompra?> FindByIdAsync(int id)
        {
            var response = await _context.Set<DetalleCompra>()
                .Include(x => x.Compra)
                .FirstOrDefaultAsync(x => x.IdDetalleCompra == id);

            return response;
        }


        public async override Task<IReadOnlyList<DetalleCompra>> FindAllAsync()
        {
            return await _context.Set<DetalleCompra>()
                                 .Include(c => c.Compra)
                                 .AsNoTracking()
                                 .ToListAsync();
        }


    }
}



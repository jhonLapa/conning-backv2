using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MenuRepositorio : CrudCoreRespository<Menu, int>, IMenuRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public MenuRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<PaginadoResponse<Menu>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<Menu>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Name) : contex.OrderBy(p => p.Name),
                    "descripcion" => order == "desc" ? contex.OrderByDescending(p => p.Icon) : contex.OrderBy(p => p.Icon),
                    "url" => order == "desc" ? contex.OrderByDescending(p => p.Url) : contex.OrderBy(p => p.Url),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.State) : contex.OrderBy(p => p.State),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.AuditCreateDate) : contex.OrderBy(p => p.AuditCreateDate),
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
                        if (value == "activo") contex = contex.Where(p => p.State == true);
                        if (value == "inactivo") contex = contex.Where(p => p.State == false);
                    }
                    else if (id == "nombre") contex = contex.Where(p => p.Name.Contains(value));

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


            PaginadoResponse<Menu> response = new(data, meta);

            return response;
        }

        public async Task<IReadOnlyList<Menu>> SelectActivo()
        {
            return await _context.Set<Menu>()
                                 .AsNoTracking()
                                 .Where(a => a.State == true)
                                 .ToListAsync();
        }

    }
}

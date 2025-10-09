using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PermissionRespositorio : CrudCoreRespository<Permission, int>, IPermissionRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PermissionRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<Permission>> BusquedaPaginado(PaginationRequest dto)
        {

            var contex = _context.Set<Permission>()
              .Include(c => c.Menu)
              .AsQueryable();


            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Name) : contex.OrderBy(p => p.Name),
                    "description" => order == "desc" ? contex.OrderByDescending(p => p.Description) : contex.OrderBy(p => p.Description),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.State) : contex.OrderBy(p => p.State),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.AuditCreateDate) : contex.OrderBy(p => p.AuditCreateDate),
                    _ => contex
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
                        if (value == "activo") contex = contex.Where(p => p.State == 1);
                        if (value == "inactivo") contex = contex.Where(p => p.State == 0);
                    }
                    else if (id == "name") contex = contex.Where(p => p.Name.Contains(value));

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


            PaginadoResponse<Permission> response = new(data, meta);

            return response;
        }


        public async Task<IReadOnlyList<Permission>> SelectActivo()
        {
            return await _context.Set<Permission>()
                                 .AsNoTracking()
                                 .Where(a => a.State == 1)
                                 .ToListAsync();
        }

        public async override Task<Permission?> FindByIdAsync(int id)
        {
            return await _context.Set<Permission>()
                                 .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                 .Include(c => c.Menu)
                                 .FirstOrDefaultAsync(x => x.PermissionId == id);
        }



        public async override Task<IReadOnlyList<Permission>> FindAllAsync()
        {
            return await _context.Set<Permission>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Menu)
                                 .ToListAsync();
        }

        public async Task<List<Permission>> FindByMenuIdAsync(int menuId)
        {
            return await _context.Set<Permission>()
                .AsNoTracking()
                .AsSplitQuery() // evita el warning MultipleCollectionInclude
                .Include(v => v.Menu)
                .Where(v => v.MenuId == menuId)
                .ToListAsync();
        }
    }
}
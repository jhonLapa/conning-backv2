using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MenuRoleRespositorio : CrudCoreRespository<MenuRole, int>, IMenuRoleRepositorio
    {
        private readonly ApplicationDbContext _context;
        public MenuRoleRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<MenuRole>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<MenuRole>()
                     .Include(c => c.Menus)
                     .Include(c => c.Roles)
                     .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
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


            PaginadoResponse<MenuRole> response = new(data, meta);

            return response;
        }

        public async Task<IReadOnlyList<MenuRole>> SelectActivo()
        {
            return await _context.Set<MenuRole>()
                                 .AsNoTracking()
                                 .Where(a => a.State == true)
                                 .ToListAsync();
        }


        public async override Task<MenuRole?> FindByIdAsync(int id)
        {
            return await _context.Set<MenuRole>()
                                  .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                  .Include(c => c.Menus)
                                  .Include(c => c.Roles)
                                  .FirstOrDefaultAsync(x => x.MenuRoleId == id);
        }


        public async override Task<IReadOnlyList<MenuRole>> FindAllAsync()
        {
            return await _context.Set<MenuRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Menus)
                                 .Include(c => c.Roles)
                                 .ToListAsync();
        }

        public async Task<List<MenuRole>> FindByMenuIdAsync(int menuId)
        {
            return await _context.Set<MenuRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Menus)
                                 .Where(v => v.MenuId == menuId)
                                 .ToListAsync();
        }

        public async Task<List<MenuRole>> FindByRolIdAsync(int roleId)
        {
            return await _context.Set<MenuRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Roles)
                                 .Where(v => v.RoleId == roleId)
                                 .ToListAsync();
        }
    }
}

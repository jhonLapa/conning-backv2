using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserRoleRespositorio : CrudCoreRespository<UserRole, int>, IUserRoleRepositorio
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<UserRole>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<UserRole>()
                     .Include(c => c.Users)
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
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.AuditCreateDate): contex.OrderBy(p => p.AuditCreateDate),
                    _ => contex.OrderBy(p => p.UserRoleId),
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


            PaginadoResponse<UserRole> response = new(data, meta);

            return response;
        }

        public async Task<IReadOnlyList<UserRole>> SelectActivo()
        {
            return await _context.Set<UserRole>()
                                 .AsNoTracking()
                                 .Where(a => a.State == true)
                                 .ToListAsync();
        }


        public async override Task<UserRole?> FindByIdAsync(int id)
        {
            return await _context.Set<UserRole>()
                                  .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                  .Include(c => c.Users)
                                  .Include(c => c.Roles)
                                  .FirstOrDefaultAsync(x => x.UserRoleId == id);
        }


        public async  Task<UserRole?> FindByIdAsyncUser(int id)
        {
            return await _context.Set<UserRole>()
                                  .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                  .Include(c => c.Users)
                                  .Include(c => c.Roles)
                                  .FirstOrDefaultAsync(x => x.UserId == id);
        }



        public async override Task<IReadOnlyList<UserRole>> FindAllAsync()
        {
            return await _context.Set<UserRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Users)
                                 .Include(c => c.Roles)
                                 .ToListAsync();
        }

        public async Task<List<UserRole>> FindByUserIdAsync(int userId)
        {
            return await _context.Set<UserRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Users)
                                 .Where(v => v.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<List<UserRole>> FindByRolIdAsync(int roleId)
        {
            return await _context.Set<UserRole>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Roles)
                                 .Where(v => v.RoleId == roleId)
                                 .ToListAsync();
        }
    }
}

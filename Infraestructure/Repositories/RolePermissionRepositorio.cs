using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RolePermissionRespositorio : CrudCoreRespository<RolePermission, int>, IRolePermissionRepositorio
    {
        private readonly ApplicationDbContext _context;
        public RolePermissionRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<RolePermission>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<RolePermission>()
                     .Include(c => c.Permissions)
                     .Include(c => c.Roles)
                     .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "permissionId" => order == "desc" ? contex.OrderByDescending(p => p.PermissionId) : contex.OrderBy(p => p.PermissionId),
                    "roleId" => order == "desc" ? contex.OrderByDescending(p => p.RoleId) : contex.OrderBy(p => p.RoleId),
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


            PaginadoResponse<RolePermission> response = new(data, meta);

            return response;
        }

        public async Task AttachUnchangedAsync(int roleId, int permissionId)
        {
            var roleStub = new Domain.Rol { RoleId = roleId };
            _context.Entry(roleStub).State = EntityState.Unchanged;

            var permissionStub = new Domain.Permission { PermissionId = permissionId };
            _context.Entry(permissionStub).State = EntityState.Unchanged;

            await Task.CompletedTask;
        }

        public async Task<RolePermission> FindByIdAsync(int roleId, int permissionId)
        {
            return await _context.Set<RolePermission>()
                .Include(rp => rp.Roles)
                .Include(rp => rp.Permissions)
                .AsNoTracking()
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }
        public new async Task SaveAsync(RolePermission entity)
        {
            await base.SaveAsync(entity);
        }


        public async Task<IReadOnlyList<RolePermission>> SelectActivo()
        {
            return await _context.Set<RolePermission>()
                                 .AsNoTracking()
                                 .Where(a => a.State == 1)
                                 .ToListAsync();
        }
        


        public async override Task<IReadOnlyList<RolePermission>> FindAllAsync()
        {
            return await _context.Set<RolePermission>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Permissions)
                                 .Include(c => c.Roles)
                                 .ToListAsync();
        }

        public async Task<List<RolePermission>> FindByPermissionIdAsync(int permissionId)
        {
            return await _context.Set<RolePermission>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Permissions)
                                 .Where(v => v.PermissionId == permissionId)
                                 .ToListAsync();
        }

        public async Task<List<RolePermission>> FindByRolIdAsync(int roleId)
        {
            return await _context.Set<RolePermission>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // evita el warning MultipleCollectionInclude
                                 .Include(v => v.Roles)
                                 .Where(v => v.RoleId == roleId)
                                 .ToListAsync();
        }
    }
}

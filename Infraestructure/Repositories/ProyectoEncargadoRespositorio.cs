using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ProyectoEncargadoRespositorio : CrudCoreRespository<ProyectoEncargado, int>, IProyectoEncargadoRepositorio
    {
        private readonly ApplicationDbContext _context;
        public ProyectoEncargadoRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<ProyectoEncargado>> BusquedaPaginado(PaginationRequest dto)
        {

            var contex = _context.Set<ProyectoEncargado>()
              .Include(c => c.Trabajador)
              .Include(c => c.Proyecto)
              .AsQueryable();


            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Rol) : contex.OrderBy(p => p.Rol),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.Estado) : contex.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.FechaInicio) : contex.OrderBy(p => p.FechaInicio),
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
                    else if (id == "name") contex = contex.Where(p => p.Rol.Contains(value));

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


            PaginadoResponse<ProyectoEncargado> response = new(data, meta);

            return response;
        }


        public async Task<IReadOnlyList<ProyectoEncargado>> SelectActivo()
        {
            return await _context.Set<ProyectoEncargado>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<ProyectoEncargado?> FindByIdAsync(int id)
        {
            return await _context.Set<ProyectoEncargado>()
                                 .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                 .Include(c => c.Trabajador)
                                 .Include(c => c.Proyecto)
                                 .FirstOrDefaultAsync(x => x.IdProyectoEncargado == id);
        }



        public async override Task<IReadOnlyList<ProyectoEncargado>> FindAllAsync()
        {
            return await _context.Set<ProyectoEncargado>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Trabajador)
                                 .Include(c => c.Proyecto)
                                 .ToListAsync();
        }
        public async Task<ProyectoEncargado?> FindByProyectoAsync(int idProyecto)
        {
            return await _context.Set<ProyectoEncargado>()
                .FirstOrDefaultAsync(x => x.IdProyecto == idProyecto);
        }

        public async Task DeleteByProyectoIdAsync(int idProyecto)
        {
            var registros = await _context.Set<ProyectoEncargado>()
                .Where(x => x.IdProyecto == idProyecto)
                .ToListAsync();

            if (registros.Any())
            {
                _context.Set<ProyectoEncargado>().RemoveRange(registros);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProyectoEncargado?> FindLastByProyectoAsync(int idProyecto)
        {
            return await _context.Set<ProyectoEncargado>()
                .Where(e => e.IdProyecto == idProyecto)
                .OrderByDescending(e => e.IdProyectoEncargado) // o FechaInicio si prefieres
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProyectoEncargado>> GetByProyectoIdAsync(int idProyecto)
        {
            return await _context.Set<ProyectoEncargado>()
                .Where(x => x.IdProyecto == idProyecto)
                .ToListAsync();
        }
    }
}
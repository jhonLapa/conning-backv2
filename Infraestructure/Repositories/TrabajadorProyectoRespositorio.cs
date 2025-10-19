using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class TrabajadorProyectoRespositorio : CrudCoreRespository<TrabajadorProyecto, int>, ITrabajadorProyectoRepositorio
    {
        private readonly ApplicationDbContext _context;
        public TrabajadorProyectoRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<TrabajadorProyecto>> BusquedaPaginado(PaginationRequest dto)
        {

            var contex = _context.Set<TrabajadorProyecto>()
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
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.UsuarioCreacion) : contex.OrderBy(p => p.UsuarioCreacion),
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
                    else if (id == "name") contex = contex.Where(p => p.UsuarioCreacion.Contains(value));

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


            PaginadoResponse<TrabajadorProyecto> response = new(data, meta);

            return response;
        }


        public async Task<IReadOnlyList<TrabajadorProyecto>> SelectActivo()
        {
            return await _context.Set<TrabajadorProyecto>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<TrabajadorProyecto?> FindByIdAsync(int id)
        {
            return await _context.Set<TrabajadorProyecto>()
                                 .AsSplitQuery() // 👈 evita el warning MultipleCollectionInclude
                                 .Include(c => c.Trabajador)
                                 .Include(c => c.Proyecto)
                                 .FirstOrDefaultAsync(x => x.IdTrabajadorProyecto == id);
        }



        public async override Task<IReadOnlyList<TrabajadorProyecto>> FindAllAsync()
        {
            return await _context.Set<TrabajadorProyecto>()
                                 .AsNoTracking()
                                 .AsSplitQuery() // 👈 importante aquí también
                                 .Include(c => c.Trabajador)
                                 .Include(c => c.Proyecto)
                                 .ToListAsync();
        }

        public async Task<TrabajadorProyecto?> FindByProyectoYTrabajadorAsync(int idProyecto, int idTrabajador)
        {
            return await _context.Set<TrabajadorProyecto>()
                .FirstOrDefaultAsync(x => x.IdProyecto == idProyecto && x.IdTrabajador == idTrabajador);
        }

        public async Task DeleteByProyectoIdAsync(int idProyecto)
        {
            var registros = await _context.Set<TrabajadorProyecto>()
                .Where(x => x.IdProyecto == idProyecto)
                .ToListAsync();

            if (registros.Any())
            {
                _context.Set<TrabajadorProyecto>().RemoveRange(registros);
                await _context.SaveChangesAsync();
            }
        }

    }
}

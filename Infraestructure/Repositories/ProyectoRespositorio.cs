using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Dtos;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ProyectoRespositorio(ApplicationDbContext context) : CrudCoreRespository<Proyecto, int>(context), IProyectoRepositorio
    {
        public async Task<PaginadoResponse<Proyecto>> BusquedaPaginado(PaginationRequest dto)
        {

            var contex = _context.Set<Proyecto>()
                 .Include(c => c.Cliente)
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Nombre) : contex.OrderBy(p => p.Nombre),
                    "descripcion" => order == "desc" ? contex.OrderByDescending(p => p.Descripcion) : contex.OrderBy(p => p.Descripcion),
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
                    else if (id == "nombre") contex = contex.Where(p => p.Nombre.Contains(value));

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


            PaginadoResponse<Proyecto> response = new(data, meta);

            return response;
        }


            public async Task<PaginadoResponse<ProyectoPlanillaTotalDto>> BusquedaPaginadoTrabajador(
                PaginationRequest dto,
                int idTrabajador)
        {
            var query = _context.Set<Proyecto>()
                .Include(p => p.Planillas)
                .Include(p => p.TrabajadoresProyectos)
                .Where(p => p.TrabajadoresProyectos.Any(tp => tp.IdTrabajador == idTrabajador))
                .AsQueryable();

            // 🔹 Orden dinámico
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var parts = dto.Sort.Split(".");
                var column = parts[0];
                var order = parts[1];

                query = column switch
                {
                    "name" => order == "desc" ? query.OrderByDescending(p => p.Nombre) : query.OrderBy(p => p.Nombre),
                    "descripcion" => order == "desc" ? query.OrderByDescending(p => p.Descripcion) : query.OrderBy(p => p.Descripcion),
                    "status" => order == "desc" ? query.OrderByDescending(p => p.Estado) : query.OrderBy(p => p.Estado),
                    "createAt" => order == "desc" ? query.OrderByDescending(p => p.FechaCreacion) : query.OrderBy(p => p.FechaCreacion),
                    _ => query
                };
            }

            // 🔹 Filtros
            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");
                    var id = id_value[0];
                    var value = id_value[1];

                    if (id == "status")
                    {
                        if (value == "activo") query = query.Where(p => p.Estado == 1);
                        if (value == "inactivo") query = query.Where(p => p.Estado == 0);
                    }
                    else if (id == "nombre")
                    {
                        query = query.Where(p => p.Nombre.Contains(value));
                    }
                }
            }

            // 🔹 Paginación
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;
            var total = await query.CountAsync();

            // 🔹 Selección final usando el DTO
            var data = await query
                .Skip(skip)
                .Take(take)
                .Select(p => new ProyectoPlanillaTotalDto
                {
                    IdProyecto = p.IdProyecto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Estado = p.Estado,
                    FechaCreacion = p.FechaCreacion,
                    TotalPlanillas = p.Planillas.Sum(pl => (decimal?)pl.TotalGeneral) ?? 0
                })
                .ToListAsync();

            var meta = new Meta
            {
                Page = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<ProyectoPlanillaTotalDto>(data, meta);
        }

        public async Task<IReadOnlyList<Proyecto>> SelectActivo()
        {
            return await _context.Set<Proyecto>()
                                 .AsNoTracking()
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<Proyecto?> FindByIdAsync(int id)
        {
            var proyecto = await _context.Set<Proyecto>()
                .Include(x => x.Cliente)
                .Include(x => x.TrabajadoresProyectos)
                    .ThenInclude(t => t.Trabajador)
                .Include(x => x.AportesSindicato)
                .Include(x => x.proyectoEncargados)
                    .ThenInclude(t => t.Trabajador)
                .FirstOrDefaultAsync(x => x.IdProyecto == id);

            if (proyecto != null && proyecto.proyectoEncargados.Any())
            {
                // 🔹 Dejar solo el último encargado según la fecha más reciente
                proyecto.proyectoEncargados = proyecto.proyectoEncargados
                    .OrderByDescending(e => e.FechaInicio)
                    .Take(1)
                    .ToList();
            }

            return proyecto;
        }


        public async override Task<IReadOnlyList<Proyecto>> FindAllAsync()
        {
            return await _context.Set<Proyecto>()
                                 .Include(c => c.Cliente)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

    }
}

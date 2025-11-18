using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class TrabajadorRepositorio : CrudCoreRespository<Trabajador, int>, ITrabajadorRepositorio
    {
        private readonly ApplicationDbContext _context;
        public TrabajadorRepositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<Trabajador>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<Trabajador>()
                        .Include(t => t.Categoria)
                        .Include(t => t.Regimen)
                        .Include(t => t.TipoDocumento)
                        .AsQueryable();


            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.ApellidosNombres) : contex.OrderBy(p => p.ApellidosNombres),
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
                    else if (id == "name") contex = contex.Where(p => p.ApellidosNombres.Contains(value));

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


            PaginadoResponse<Trabajador> response = new(data, meta);

            return response;
        }

        public async Task<IReadOnlyList<Trabajador>> SelectActivo()
        {
            return await _context.Set<Trabajador>()
                                 .AsNoTracking()
                        .Include(t => t.Categoria)
                                 .Where(a => a.Estado == 1)
                                 .ToListAsync();
        }

        public async override Task<IReadOnlyList<Trabajador>> FindAllAsync()
        {
            return await _context.Set<Trabajador>()
                .Include(t => t.Categoria)
                .Include(t => t.Regimen)
                .Include(t => t.TipoDocumento)
                .Include(t => t.CuentasBancarias) // ✅ incluir cuentas
                    .ThenInclude(cb => cb.Banco)   // opcional: también el banco
                .AsNoTracking()
                .ToListAsync();
        }

        public async override Task<Trabajador?> FindByIdAsync(int id)
        {
            var response = await _context.Set<Trabajador>()
                .Include(t => t.Categoria)
                .Include(t => t.Regimen)
                .Include(t => t.TipoDocumento)
                .Include(t => t.CuentasBancarias) 
                    .ThenInclude(cb => cb.Banco)
                .FirstOrDefaultAsync(t => t.IdTrabajador == id);

            return response;
        }

        public async Task<IReadOnlyList<Trabajador>> SelectByProyecto(int idProyecto)
        {
            return await _context.Set<Trabajador>()
                .AsNoTracking()
                .Include(t => t.Categoria)
                .Include(t => t.TrabajosProyectos.Where(tp => tp.IdProyecto == idProyecto)) // 👈 filtrado interno
                    .ThenInclude(tp => tp.Proyecto)
                .Where(t => t.Estado == 1 &&
                            t.TrabajosProyectos.Any(tp => tp.IdProyecto == idProyecto))
                .ToListAsync();
        }
        public async Task<PaginadoResponse<Trabajador>> BusquedaPaginadoConPlanilla(
                    PaginationRequest dto,
                    DateTime? fechaInicio = null,
                    DateTime? fechaFin = null)
        {
            var query = _context.Set<Trabajador>()
                .Include(t => t.Categoria)
                .Include(t => t.Regimen)
                .Include(t => t.TipoDocumento)
                .Include(t => t.TrabajosProyectos)
                    .ThenInclude(tp => tp.Detalles)
                        .ThenInclude(d => d.Planilla)
                .AsQueryable();

            // 🔹 Solo trabajadores con al menos una planilla
            query = query.Where(t =>
                t.TrabajosProyectos.Any(tp =>
                    tp.Detalles.Any(d => d.Planilla != null)));

            // 🔹 Filtro opcional por rango de fechas (FechaPago de la planilla)
            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(t =>
                    t.TrabajosProyectos.Any(tp =>
                        tp.Detalles.Any(d =>
                            d.Planilla != null &&
                            d.Planilla.FechaPago >= fechaInicio &&
                            d.Planilla.FechaPago <= fechaFin)));
            }
            else if (fechaInicio.HasValue)
            {
                query = query.Where(t =>
                    t.TrabajosProyectos.Any(tp =>
                        tp.Detalles.Any(d =>
                            d.Planilla != null &&
                            d.Planilla.FechaPago >= fechaInicio)));
            }
            else if (fechaFin.HasValue)
            {
                query = query.Where(t =>
                    t.TrabajosProyectos.Any(tp =>
                        tp.Detalles.Any(d =>
                            d.Planilla != null &&
                            d.Planilla.FechaPago <= fechaFin)));
            }

            // 🔹 Ordenamiento
            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var parts = dto.Sort.Split(".");
                var column = parts[0];
                var order = parts.ElementAtOrDefault(1) ?? "asc";

                query = column switch
                {
                    "name" => order == "desc"
                        ? query.OrderByDescending(t => t.ApellidosNombres)
                        : query.OrderBy(t => t.ApellidosNombres),
                    "status" => order == "desc"
                        ? query.OrderByDescending(t => t.Estado)
                        : query.OrderBy(t => t.Estado),
                    "createAt" => order == "desc"
                        ? query.OrderByDescending(t => t.FechaCreacion)
                        : query.OrderBy(t => t.FechaCreacion),
                    _ => query.OrderByDescending(t => t.FechaCreacion)
                };
            }

            // 🔹 Filtros adicionales
            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");
                    var id = id_value[0];
                    var value = id_value[1];

                    if (id == "status")
                    {
                        if (value == "activo") query = query.Where(t => t.Estado == 1);
                        if (value == "inactivo") query = query.Where(t => t.Estado == 0);
                    }
                    else if (id == "name")
                    {
                        query = query.Where(t => t.ApellidosNombres.Contains(value));
                    }
                }
            }

            // 🔹 Paginación
            var take = dto.Take ?? 5;
            var page = dto.Page ?? 1;
            var skip = (page - 1) * take;

            var total = await query.CountAsync();
            var data = await query.Skip(skip).Take(take).ToListAsync();

            var meta = new Meta
            {
                Page = page,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / take)
            };

            return new PaginadoResponse<Trabajador>(data, meta);
        }


    }
}

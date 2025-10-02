using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MovimientoEspecialRespositorio : CrudCoreRespository<MovimientoEspecial, int>, IMovimientoEspecialRepositorio
    {
        private readonly ApplicationDbContext _context;
        public MovimientoEspecialRespositorio(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<PaginadoResponse<MovimientoEspecial>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<MovimientoEspecial>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Descripcion) : contex.OrderBy(p => p.Descripcion),
                    "monto" => order == "desc" ? contex.OrderByDescending(p => p.Monto) : contex.OrderBy(p => p.Monto),
                    "tipoMovimiento" => order == "desc" ? contex.OrderByDescending(p => p.TipoMovimiento) : contex.OrderBy(p => p.TipoMovimiento),
                    "cuentaBancaria" => order == "desc" ? contex.OrderByDescending(p => p.CuentaBancaria) : contex.OrderBy(p => p.CuentaBancaria),
                    "observacion" => order == "desc" ? contex.OrderByDescending(p => p.Observacion) : contex.OrderBy(p => p.Observacion),
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
                    else if (id == "descripcion") contex = contex.Where(p => p.Descripcion.Contains(value));

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


            PaginadoResponse<MovimientoEspecial> response = new(data, meta);

            return response;
        }

    }
}

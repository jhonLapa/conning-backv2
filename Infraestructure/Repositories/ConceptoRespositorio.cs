using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ConceptoRespositorio
        : CrudCoreRespository<Concepto, int>, IConceptoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public ConceptoRespositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override async Task<Concepto> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Concepto>().Include(t => t.Grupo).FirstOrDefaultAsync(t => t.IdConcepto == id);
        }
        public async Task<IReadOnlyList<Concepto>> FecthConceptoByIdGrupo(int idGrupo)
        {
            return await _dbContext.Set<Concepto>().Where(t => t.IdGrupo == idGrupo).ToListAsync();
        }
        public async Task<PaginadoResponse<Concepto>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _context.Set<Concepto>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");

                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "code" => order == "desc" ? contex.OrderByDescending(p => p.Codigo) : contex.OrderBy(p => p.Descripcion),
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
                    else if (id == "code") contex = contex.Where(p => p.Codigo.Contains(value));

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


            PaginadoResponse<Concepto> response = new(data, meta);

            return response;
        }



    }
}

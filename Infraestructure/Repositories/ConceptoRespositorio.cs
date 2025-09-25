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

        public async Task<IReadOnlyList<Concepto>> FecthConceptoByIdGrupo(int idGrupo)
        {
            return await _dbContext.Set<Concepto>().Where(t => t.IdGrupo == idGrupo).ToListAsync();
        }

    }
}

using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ConceptoAfectacionRespositorio
        : CrudCoreRespository<ConceptoAfectacion, int>, IConceptoAfectacionRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public ConceptoAfectacionRespositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<ConceptoAfectacion?> FindByConceptoAndAfectacionAsync(int IdConcepto, int idAfectacion)
        {
            return await _dbContext.Set<ConceptoAfectacion>()
                .FirstOrDefaultAsync(c => c.IdConcepto == IdConcepto && c.IdAfectacion == idAfectacion);
        }

    }
}

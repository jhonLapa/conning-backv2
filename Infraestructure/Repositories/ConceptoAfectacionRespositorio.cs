using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

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

    }
}

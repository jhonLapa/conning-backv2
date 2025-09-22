using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class GrupoConceptoRespositorio
        : CrudCoreRespository<GrupoConcepto, int>, IGrupoConceptoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public GrupoConceptoRespositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

    }
}
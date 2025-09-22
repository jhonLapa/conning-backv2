using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class AfectacionRespositorio(ApplicationDbContext context) : CrudCoreRespository<Afectacion, int>(context), IAfectacionRepositorio
    {

    }
}



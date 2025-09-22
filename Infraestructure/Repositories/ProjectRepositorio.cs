using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class ProjectRepositorio(ApplicationDbContext context): CrudCoreRespository<Project, int>(context) , IProjectRepositorio
    {
    }
}

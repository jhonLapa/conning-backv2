using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class ProyectoRepositorio(ApplicationDbContext context): CrudCoreRespository<Proyecto, int>(context) , IProyectoRepositorio
    {
    }
}

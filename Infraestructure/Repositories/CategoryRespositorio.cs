using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class CategoryRespositorio(ApplicationDbContext context) : CrudCoreRespository<Category, int>(context), ICategoryRepositorio
    {

    }
}

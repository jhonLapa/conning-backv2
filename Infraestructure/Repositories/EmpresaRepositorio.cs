using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class EmpresaRespositorio(ApplicationDbContext context) : CrudCoreRespository<Empresa, int>(context), IEmpresaRepositorio
    {

    }
}

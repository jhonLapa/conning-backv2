using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class BankRespositorio(ApplicationDbContext context) : CrudCoreRespository<Bank , int>(context), IBankRepositorio
    {
     
    }
}

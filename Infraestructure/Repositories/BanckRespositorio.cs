using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class BanckRespositorio(ApplicationDbContext context) : CrudCoreRespository<Banck , int>(context), IBanckRepositorio
    {
     
    }
}

using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class CuentaBancariaTrabajadorRespositorio: CrudCoreRespository<CuentaBancariaTrabajador, int> , ICuentaBancariaTrabajadorRepositorio
    {
        private readonly ApplicationDbContext _context;
        public CuentaBancariaTrabajadorRespositorio(ApplicationDbContext context) : base(context)
        { 
            _context = context;   
        }

        public override async Task<IReadOnlyList<CuentaBancariaTrabajador>> FindAllAsync()
        {
            var response = await _context.Set<CuentaBancariaTrabajador>().
                Include(e => e.Banco).
                ToListAsync();

            return response;
        }
    }
}

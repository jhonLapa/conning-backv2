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
    public class EmployeeBankAccountRepositorio: CrudCoreRespository<EmployeeAccountBanck, int> , IEmployeeBankAccountRepositorio
    {
        private readonly ApplicationDbContext _context;
        public EmployeeBankAccountRepositorio(ApplicationDbContext context) : base(context)
        { 
            _context = context;   
        }

        public override async Task<IReadOnlyList<EmployeeAccountBanck>> FindAllAsync()
        {
            var response = await _context.Set<EmployeeAccountBanck>().
                Include(e => e.Banck).
                ToListAsync();

            return response;
        }
    }
}

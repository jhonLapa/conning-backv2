using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RolRepositorio : CrudCoreRespository<Rol, int>, IRolRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public RolRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Rol> FillName(string name)
        {
            var response = await _context.Set<Rol>().FirstOrDefaultAsync(t => t.Name.Equals(name));

            return response;
        }
       
    }
}

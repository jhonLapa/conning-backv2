using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UsuarioRepository : CrudCoreRespository<User, int>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .Where(t => t.Email.ToUpper().Equals(email.ToUpper()))
                .FirstOrDefaultAsync();
        }

        

        public async Task<User?> FindByPersonaAsync(string idPersona)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync();
        }

        public async override Task<User?> FindByIdAsync(int id)
        {
            return await _context.Set<User>()
                .Where(t => t.UserId == id)
                .FirstOrDefaultAsync();
        }
    }
}

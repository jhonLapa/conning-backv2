using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> GetAllAsync()
        {
            return await _context.Set<Departamento>()
                .AsNoTracking()
                .OrderBy(x => x.Nombre)
                .ToListAsync();
        }

        public async Task<Departamento?> GetByIdAsync(int id)
        {
            return await _context.Set<Departamento>()
                .FirstOrDefaultAsync(x => x.DepartamentoId == id);
        }

        public async Task<Departamento> AddAsync(Departamento entity)
        {
            _context.Set<Departamento>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Departamento entity)
        {
            _context.Set<Departamento>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Departamento entity)
        {
            _context.Set<Departamento>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
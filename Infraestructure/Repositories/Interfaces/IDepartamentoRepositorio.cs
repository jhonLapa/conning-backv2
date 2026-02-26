using Domain;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDepartamentoRepositorio
    {
        Task<List<Departamento>> GetAllAsync();
        Task<Departamento?> GetByIdAsync(int id);
        Task<Departamento> AddAsync(Departamento entity);
        Task UpdateAsync(Departamento entity);
        Task DeleteAsync(Departamento entity);
    }
}

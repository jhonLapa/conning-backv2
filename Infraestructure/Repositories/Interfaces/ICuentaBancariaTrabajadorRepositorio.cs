using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICuentaBancariaTrabajadorRepositorio : ICrudCoreRespository<CuentaBancariaTrabajador, int>
    {
        Task DeleteByCuentaTrabajadorIdAsync(int id);
        Task<IEnumerable<CuentaBancariaTrabajador>> GetByTrabajadorIdAsync(int idTrabajador);

    }
}

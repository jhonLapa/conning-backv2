using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ICuentaBancariaTrabajadorRepositorio : ICrudCoreRespository<CuentaBancariaTrabajador, int>
    {
        Task DeleteAsync(int id);
        Task<IReadOnlyList<CuentaBancariaTrabajador>> FindByTrabajadorIdAsync(int idTrabajador);
    }
}

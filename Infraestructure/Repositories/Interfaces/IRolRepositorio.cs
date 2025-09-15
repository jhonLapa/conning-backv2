using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRolRepositorio : ICrudCoreRespository<Rol, int>
    {
        Task<Rol> FillName(string name);
    }
}

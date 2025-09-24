using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IConfigAfectacionRepositorio : ICrudCoreRespository<ConfigAfectacion, int>
    {
        Task<ConfigAfectacion> FindByEmpresaAndAfectacionAsync(int idEmpresa, int idAfectacion);
        Task<IReadOnlyList<ConfigAfectacion>> FechtByIdEmpresa(int idEmpresa);
    }
}

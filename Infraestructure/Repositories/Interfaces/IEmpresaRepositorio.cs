using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IEmpresaRepositorio : ICrudCoreRespository<Empresa, int>
    {
        Task<PaginadoResponse<Empresa>> BusquedaPaginado(PaginationRequest dto);
    }
}

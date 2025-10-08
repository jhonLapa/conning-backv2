using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IAsistenciaRepositorio : ICrudCoreRespository<Asistencia, int>
    {
        Task<PaginadoResponse<Asistencia>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<Asistencia>> SelectActivo();
    }
}
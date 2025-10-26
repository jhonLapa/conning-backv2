using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMovimientoEspecialRepositorio : ICrudCoreRespository<MovimientoEspecial, int>
    {
        Task<PaginadoResponse<MovimientoEspecial>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<IReadOnlyList<MovimientoEspecial>> SelectActivo();

    }
}


using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPlanillaRepositorio : ICrudCoreRespository<Planilla, int>
    {
        Task<PaginadoResponse<Planilla>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);

    }
}



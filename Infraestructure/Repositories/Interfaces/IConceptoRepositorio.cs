
using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IConceptoRepositorio : ICrudCoreRespository<Concepto, int>
    {
        Task<IReadOnlyList<Concepto>> FecthConceptoByIdGrupo(int idGrupo);
        Task<PaginadoResponse<Concepto>> BusquedaPaginado(PaginationRequest dto);
    }
}



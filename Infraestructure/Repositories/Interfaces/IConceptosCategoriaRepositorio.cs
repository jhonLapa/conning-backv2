using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IConceptosCategoriaRepositorio : ICrudCoreRespository<ConceptosCategoria, int>
    {
        Task<PaginadoResponse<ConceptosCategoria>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ConceptosCategoria>> SelectActivo();
    }
}

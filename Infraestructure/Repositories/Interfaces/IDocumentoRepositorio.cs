using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDocumentoRepositorio : ICrudCoreRespository<DocumentType, int>
    {
        Task<PaginadoResponse<DocumentType>> BusquedaPaginado(PaginationRequest dto);

    }
}

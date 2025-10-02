using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.TiposDocumento;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ITipoDocumentoService : ICrudCoreService<TipoDocumentoDto, TipoDocumentoSaveDto, int>
    {
        Task<PaginadoResponse<TipoDocumentoDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TipoDocumentoSelectDto>> SelectActivo();
    }
}

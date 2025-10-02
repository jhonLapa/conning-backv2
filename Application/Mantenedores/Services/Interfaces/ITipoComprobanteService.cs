using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ITipoComprobanteService : ICrudCoreService<TipoComprobanteDto, TipoComprobanteSaveDto, int>
    {
        Task<PaginadoResponse<TipoComprobanteDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TipoComprobanteSelectDto>> SelectActivo();

    }
}

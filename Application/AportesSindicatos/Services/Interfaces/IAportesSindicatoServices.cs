using Application.AportesSindicatos.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.AportesSindicatos.Services.Interfaces
{
    public interface IAportesSindicatoServices : ICrudCoreService<AportesSindicatoDto, AportesSindicatoSaveDto, int>
    {
        Task<IReadOnlyList<AportesSindicatoSelectDto>> SelectActivo();
        Task<PaginadoResponse<AportesSindicatoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
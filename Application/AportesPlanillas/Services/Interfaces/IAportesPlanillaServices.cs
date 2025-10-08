using Application.AportesPlanillas.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.AportesPlanillas.Services.Interfaces
{
    public interface IAportesPlanillaServices : ICrudCoreService<AportesPlanillaDto, AportesPlanillaSaveDto, int>
    {
        Task<IReadOnlyList<AportesPlanillaSelectDto>> SelectActivo();
        Task<PaginadoResponse<AportesPlanillaDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
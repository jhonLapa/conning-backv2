using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.GrupoConceptos;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IGrupoConceptoService : ICrudCoreService<GrupoConceptoDto, GrupoConceptoSaveDto, int>
    {
        Task<IReadOnlyList<GrupoConceptoSelectDto>> SelectGrupoConcepto();
        Task<PaginadoResponse<GrupoConceptoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

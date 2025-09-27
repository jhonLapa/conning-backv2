using Application.Conceptos.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Conceptos.Services.Interfaces
{
    public interface IConceptoServices : ICrudCoreService<ConceptoDto, ConceptoSaveDto, int>
    {
        Task<IReadOnlyList<ConceptoDto>> FecthConceptoByIdGrupo(int idGrupo);
        Task<PaginadoResponse<ConceptoDto>> BusquedaPaginado(PaginationRequest dto);

    }
}


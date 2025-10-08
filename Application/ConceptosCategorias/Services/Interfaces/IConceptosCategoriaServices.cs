using Application.ConceptosCategorias.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.ConceptosCategorias.Services.Interfaces
{
    public interface IConceptosCategoriaServices : ICrudCoreService<ConceptosCategoriaDto, ConceptosCategoriaSaveDto, int>
    {
        Task<IReadOnlyList<ConceptosCategoriaSelectDto>> SelectActivo();
        Task<PaginadoResponse<ConceptosCategoriaDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
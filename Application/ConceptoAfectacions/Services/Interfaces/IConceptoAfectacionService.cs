using Application.ConceptoAfectacions.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.ConceptoAfectacions.Services.Interfaces
{
    public interface IConceptoAfectacionServices : ICrudCoreService<ConceptoAfectacionDto, ConceptoAfectacionSaveDto, int>
    {
        Task<OperationResult<IEnumerable<ConceptoAfectacionDto>>> SaveArrayAsync(IEnumerable<ConceptoAfectacionSaveDto> saveDtos);
    }
}


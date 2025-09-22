using Application.Core.Services.Interfaces;
using Application.ConceptoAfectacions.Dto;

namespace Application.ConceptoAfectacions.Services.Interfaces
{
    public interface IConceptoAfectacionServices : ICrudCoreService<ConceptoAfectacionDto, ConceptoAfectacionSaveDto, int>
    {
    }
}


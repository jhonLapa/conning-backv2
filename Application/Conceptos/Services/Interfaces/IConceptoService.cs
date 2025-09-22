using Application.Core.Services.Interfaces;
using Application.Conceptos.Dto;

namespace Application.Conceptos.Services.Interfaces
{
    public interface IConceptoServices : ICrudCoreService<ConceptoDto, ConceptoSaveDto, int>
    {
    }
}


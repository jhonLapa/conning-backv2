using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.GrupoConceptos;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IGrupoConceptoService : ICrudCoreService<GrupoConceptoDto, GrupoConceptoSaveDto, int>
    {
    }
}

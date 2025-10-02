using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IRegimenPrevisionalService : ICrudCoreService<RegimenPrevisionalDto, RegimenPrevisionalSaveDto, int>
    {
        Task<PaginadoResponse<RegimenPrevisionalDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

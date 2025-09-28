using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.EntidadPrevisionals;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IEntidadPrevisionalService : ICrudCoreService<EntidadPrevisionalDto, EntidadPrevisionalSaveDto, int>
    {
        Task<PaginadoResponse<EntidadPrevisionalDto>> BusquedaPaginado(PaginationRequest dto);

    }
}

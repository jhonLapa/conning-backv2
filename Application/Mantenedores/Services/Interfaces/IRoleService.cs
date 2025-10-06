using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Roles;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IRolService : ICrudCoreService<RolDto, RolSaveDto, int>
    {
        Task<PaginadoResponse<RolDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<RolSelectDto>> SelectActivo();

    }
}
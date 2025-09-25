using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Afectacions;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IAfectacionService : ICrudCoreService<AfectacionDto, AfectacionSaveDto, int>
    {
        Task<PaginadoResponse<AfectacionDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

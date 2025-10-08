
using Application.Asistencias.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Asistencias.Services.Interfaces
{
    public interface IAsistenciaServices : ICrudCoreService<AsistenciaDto, AsistenciaSaveDto, int>
    {
        Task<IReadOnlyList<AsistenciaSelectDto>> SelectActivo();
        Task<PaginadoResponse<AsistenciaDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
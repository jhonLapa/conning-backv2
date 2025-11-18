using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Proyectos;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IProyectoService : ICrudCoreService<ProyectoDto , ProyectoSaveDto , int>
    {
        Task<PaginadoResponse<ProyectoDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ProyectoSelectDto>> SelectActivo();
        Task<OperationResult<ProyectoDto>> CreateProyectoCompletoAsync(ProyectoFormDataDto dto);
        Task<PaginadoResponse<ProyectoConTotalDto>> BusquedaPaginadoTrabajador(
            PaginationRequest dto,
            int idTrabajador,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null);

    }
}

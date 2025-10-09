using Application.Core.Services.Interfaces;
using Application.DetallePlanillas.Dto;
using Application.Permissions.Dto;
using Domain;

namespace Application.DetallePlanillas.Services.Interfaces
{
    public interface IDetallePlanillaServices : ICrudCoreService<DetallePlanillaDto, DetallePlanillaSaveDto, int>
    {
        Task<OperationResult<List<DetallePlanillaDto>>> ObtenerPorPlanillaAsync(int id);
        Task<OperationResult<List<DetallePlanillaDto>>> ObtenerPorTrabajadorProyectoAsync(int id);
        Task<PaginadoResponse<DetallePlanillaDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<DetallePlanillaDto>> SelectActivo();
    }
}

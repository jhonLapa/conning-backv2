using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Domain;

namespace Application.Planillas.Services.Interfaces
{
    public interface IPlanillaServices : ICrudCoreService<PlanillaDto, PlanillaSaveDto, int>
    {
        Task<PaginadoResponse<PlanillaDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<OperationResult<PlanillaDto>> CreatePlanillaCompletaAsync(PlanillaFormDataDto dto);
        Task<PaginadoResponse<PlanillaDto>> BusquedaPaginadoProyectoTrabajador(PaginationRequest dto, int idTrabajador, int idProyecto);

    }
}
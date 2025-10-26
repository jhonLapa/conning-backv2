using Application.Planillas.Dto;
using Application.Core.Services.Interfaces;
using Domain;
using Application.Mantenedores.Dtos.Planillas;

namespace Application.Planillas.Services.Interfaces
{
    public interface IPlanillaServices : ICrudCoreService<PlanillaDto, PlanillaSaveDto, int>
    {
        Task<PaginadoResponse<PlanillaDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<OperationResult<PlanillaDto>> CreatePlanillaCompletaAsync(PlanillaFormDataDto dto);

    }
}
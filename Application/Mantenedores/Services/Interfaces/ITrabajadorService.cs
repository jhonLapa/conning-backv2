using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Trabajadores;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ITrabajadorService : ICrudCoreService<TrabajadorDto, TrabajadorSaveDto, int>
    {
        Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TrabajadorSelectDto>> SelectActivo();
        Task<OperationResult<TrabajadorDto>> CreateOrUpdateWithAccountsAsync(TrabajadorWithAccountsSaveDto dto);
        Task<OperationResult<object>> GetDetallePlanillaAsync(int id);
        Task<IReadOnlyList<TrabajadorSelectDto>> SelectByProyecto(int idProyecto);
        Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginadoConPlanilla(
                PaginationRequest dto,
                DateTime? fechaInicio = null,
                DateTime? fechaFin = null);

        Task<OperationResult<object>> ProcesarCargaMasivaAsync(List<TrabajadorMasivoDto> registros);
    }
}

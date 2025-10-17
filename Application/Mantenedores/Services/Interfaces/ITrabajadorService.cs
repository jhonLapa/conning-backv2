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
    }
}

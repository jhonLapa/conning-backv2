using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IMovimientoEspecialService : ICrudCoreService<MovimientoEspecialDto, MovimientoEspecialSaveDto, int>
    {
        Task<PaginadoResponse<MovimientoEspecialDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

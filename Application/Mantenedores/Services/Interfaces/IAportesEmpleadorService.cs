using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.AportesEmpleadores;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IAportesEmpleadorService : ICrudCoreService<AportesEmpleadorDto, AportesEmpleadorSaveDto, int>
    {
        Task<PaginadoResponse<AportesEmpleadorDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

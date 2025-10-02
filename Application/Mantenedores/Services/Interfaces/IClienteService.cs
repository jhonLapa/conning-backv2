using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Clientes;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IClienteService : ICrudCoreService<ClienteDto, ClienteSaveDto, int>
    {
        Task<PaginadoResponse<ClienteDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ClienteSelectDto>> SelectActivo();

    }
}
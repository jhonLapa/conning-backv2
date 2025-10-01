using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Bancos;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IBancoService : ICrudCoreService<BancoDto, BancoSaveDto , int>
    {
        Task<PaginadoResponse<BancoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

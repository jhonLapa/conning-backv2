using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Banks;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IBankService : ICrudCoreService<BankDto, BankSaveDto , int>
    {
        Task<PaginadoResponse<BankDto>> BusquedaPaginado(PaginationRequest dto);
    }
}

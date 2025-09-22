using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Banks;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IBankService : ICrudCoreService<BankDto, BankSaveDto , int>
    {
    }
}

using Application.Core.Services.Interfaces;
using Application.Empleados.Dtos;

namespace Application.Empleados.Services.Interfaces
{
    public interface IEmployeeBankAccountServices : ICrudCoreService<EmployeeBankAccountDto, EmployeeBankAccountSaveDto , int>
    {
    }
}

using Application.Core.Services.Interfaces;
using Application.CuentasBancariasTrabajador.Dtos;

namespace Application.CuentasBancariasTrabajador.Services.Interfaces
{
    public interface IEmployeeBankAccountServices : ICrudCoreService<CuentaBancariaTrabajadorDto, CuentaBancariaTrabajadorSaveDto , int>
    {
    }
}

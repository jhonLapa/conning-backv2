using Application.Core.Services.Interfaces;
using Application.CuentasBancariasTrabajador.Dtos;

namespace Application.CuentasBancariasTrabajador.Services.Interfaces
{
    public interface ICuentaBancariaTrabajadorServices : ICrudCoreService<CuentaBancariaTrabajadorDto, CuentaBancariaTrabajadorSaveDto , int>
    {
    }
}

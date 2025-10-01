using AutoMapper;
using Domain;

namespace Application.CuentasBancariasTrabajador.Dtos.Profiles
{
    public class CuentaBancariaTrabajadorProfile : Profile
    {
        public CuentaBancariaTrabajadorProfile() 
        {

            // EmployeeBankAccount
            CreateMap<CuentaBancariaTrabajador , CuentaBancariaTrabajadorDto>().ReverseMap();
            CreateMap<CuentaBancariaTrabajador , CuentaBancariaTrabajadorSaveDto>().ReverseMap();

        }
    }
}

using Application.Empleados.Dtos.EmployeesBanksAccounts;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empleados.Dtos.Profiles
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile() 
        {

            // EmployeeBankAccount
            CreateMap<EmployeeAccountBanck , EmployeeBankAccountDto>().ReverseMap();
            CreateMap<EmployeeAccountBanck , EmployeeBankAccountSaveDto>().ReverseMap();

        }
    }
}

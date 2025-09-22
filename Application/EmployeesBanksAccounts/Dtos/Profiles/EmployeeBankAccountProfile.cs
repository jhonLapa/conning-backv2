using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empleados.Dtos.Profiles
{
    public class EmployeeBankAccountProfile : Profile
    {
        public EmployeeBankAccountProfile() 
        {

            // EmployeeBankAccount
            CreateMap<EmployeeBankAccount , EmployeeBankAccountDto>().ReverseMap();
            CreateMap<EmployeeBankAccount , EmployeeBankAccountSaveDto>().ReverseMap();

        }
    }
}

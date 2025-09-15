using Application.Core.Services.Interfaces;
using Application.Empleados.Dtos.EmployeesBanksAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empleados.Services.Interfaces
{
    public interface IEmployeeBankAccountServices : ICrudCoreService<EmployeeBankAccountDto, EmployeeBankAccountSaveDto , int>
    {
    }
}

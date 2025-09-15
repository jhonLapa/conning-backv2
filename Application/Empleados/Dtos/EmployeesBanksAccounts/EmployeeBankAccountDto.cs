using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empleados.Dtos.EmployeesBanksAccounts
{
    public class EmployeeBankAccountDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int BanckID { get; set; }
        public string AccountNumber { get; set; }
        public string CCI { get; set; }
        public string AccountType { get; set; }
        public char Currency { get; set; }
        public bool IsMain { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool State { get; set; }
        public virtual Banck Banck { get; set; }
    }
}

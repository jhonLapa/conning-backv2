using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Employee : BaseDomain
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int PensionFundId { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool State { get; set; }

        public virtual ICollection<EmployeeAccountBanck>? EmployeeBankAccounts { get; set; } 
    }
}

using Domain;

namespace Application.CuentasBancariasTrabajador.Dtos
{
    public class CuentaBancariaTrabajadorDto
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
        public virtual Banco Banck { get; set; }
    }
}

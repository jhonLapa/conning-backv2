namespace Domain
{
    public class EmployeeBankAccount
    {
        public int IdCuentaBanco { get; set; }
        public int IdTrabajador { get; set; }
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string? CCI { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public string? Moneda { get; set; }
        public bool Principal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relaciones
        public Bank Bank { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}

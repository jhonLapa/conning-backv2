namespace Domain
{
    public class CuentaBancariaTrabajador
    {
        public int IdCuentaBanco { get; set; }
        public int IdTrabajador { get; set; }
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Moneda { get; set; }
        public int Principal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // Relaciones
        public Trabajador Trabajador { get; set; }
        public Banco Banco { get; set; }
    }
}

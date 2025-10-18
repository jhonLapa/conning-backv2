namespace Application.CuentasBancariasTrabajador.Dtos
{
    public class CuentaBancariaTrabajadorSaveDto
    {
        public int IdCuentaBanco{ get; set; }
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public int Principal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}

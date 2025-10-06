namespace Application.CuentasBancariasTrabajador.Dtos
{
    public class CuentaBancariaTrabajadorSaveDto
    {
        public int IdTrabajador { get; set; }
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Moneda { get; set; }
        public int Principal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}

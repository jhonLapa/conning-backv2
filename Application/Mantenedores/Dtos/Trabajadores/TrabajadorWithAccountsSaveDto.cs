namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorWithAccountsSaveDto
    {
        public TrabajadorSaveDto Trabajador { get; set; } = null!;
        public ICollection<CuentaBancoSaveDto> Cuentas { get; set; } = new List<CuentaBancoSaveDto>();
    }

    public class CuentaBancoSaveDto
    {
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string? Cci { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public int Principal { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}

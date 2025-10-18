namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorWithAccountsSaveDto
    {
        //DATOS TRABAJADOR
        public int IdTrabajador { get; set; }
        public int IdCategoria { get; set; }
        public int IdRegimen { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string ApellidosNombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public int AsignacionFamiliar { get; set; }
        public int Hijos { get; set; }
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
        private DateTime? _fechaFin;
        public string? FechaFin
        {
            get => _fechaFin?.ToString("yyyy-MM-dd");
            set => _fechaFin = string.IsNullOrWhiteSpace(value) ? null : DateTime.Parse(value);
        }
    }
}

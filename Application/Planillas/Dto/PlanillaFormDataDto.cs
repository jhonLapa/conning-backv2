namespace Application.Mantenedores.Dtos.Planillas
{
    // 🔹 DTO principal que agrupa todo el envío
    public class PlanillaFormDataDto
    {
        public PlanillaCreateDto Planilla { get; set; } = new();
        public List<DetallePlanillaCreateDto> Detalle { get; set; } = new();
        public List<AportePlanillaDto> Aportes { get; set; } = new();
    }

    // 🔹 Encabezado principal de la planilla
    public class PlanillaCreateDto
    {
        public int IdPlanilla { get; set; }
        public int IdProyecto { get; set; }
        public string? Mes { get; set; }
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public DateTime? FechaPago { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public string FrecuenciaPago { get; set; } = string.Empty;
        public decimal TotalGeneral { get; set; }
    }

    // 🔹 Detalle de trabajadores dentro de la planilla
    public class DetallePlanillaCreateDto
    {
        public int IdTrabajadorProyecto { get; set; }
        public int DiasTrabajados { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public decimal TotalMonto { get; set; }
        public decimal TotalHoras { get; set; }
        public decimal TotalDescuentos { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public decimal? Horas60 { get; set; }
        public decimal? Horas100 { get; set; }
        public decimal? Indemnizacion { get; set; }
    }

    // 🔹 Aportes adicionales (ej. CTS, AFP, ESSALUD, Sindicato)
    public class AportePlanillaDto
    {
        public string TipoAporte { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
    }
}

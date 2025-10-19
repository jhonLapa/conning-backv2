using Application.Asistencias.Dto;

namespace Application.DetallePlanillas.Dto
{
    public class DetallePlanillaDto
    {
        public int IdDetallePlanilla { get; set; }
        public int IdPlanilla { get; set; }
        public int? IdTrabajadorProyecto { get; set; } // antes int
        public decimal? TotalDescuentos { get; set; } // antes int

        public int DiasTrabajados { get; set; }
        public int HorasTrabajadas { get; set; }
        public decimal PrimeraQuincena { get; set; }
        public decimal SegundaQuincena { get; set; }
        public decimal TotalMensual { get; set; }
        public decimal TotalHoras { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // 🔹 Relación: cada detalle puede tener muchas asistencias
        public ICollection<AsistenciaDto> Asistencias { get; set; } = new List<AsistenciaDto>();
    }
}

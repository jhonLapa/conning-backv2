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

        public decimal TotalMonto { get; set; }
        public decimal TotalHoras { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // 🔹 Relación: cada detalle puede tener muchas asistencias
    }
}

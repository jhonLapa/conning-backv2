using System.Text.Json.Serialization;

namespace Domain
{
    public class DetallePlanilla
    {
        public int IdDetallePlanilla { get; set; }
        public int IdPlanilla { get; set; }
        public int IdTrabajadorProyecto { get; set; }

        public int DiasTrabajados { get; set; }
        public int HorasTrabajadas { get; set; }
        public decimal PrimeraQuincena { get; set; }
        public decimal SegundaQuincena { get; set; }
        public decimal TotalMensual { get; set; }
        public decimal TotalHoras { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relaciones
        [JsonIgnore] // 👈 rompe el loop
        public Planilla? Planilla { get; set; }

        [JsonIgnore] // 👈 rompe el loop
        public TrabajadorProyecto? TrabajadorProyecto { get; set; }
    }
}

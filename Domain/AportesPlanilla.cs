using System.Text.Json.Serialization;

namespace Domain
{
    public class AportesPlanilla
    {
        public int IdAportePlanilla {  get; set; }
        public int IdPlanilla {  get; set; }
        public string TipoAporte {  get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }

        // 🔗 Relaciones
        [JsonIgnore] // 👈 evita ciclo Planilla <-> AportesPlanilla
        public Planilla? Planilla { get; set; }
 
    }
}

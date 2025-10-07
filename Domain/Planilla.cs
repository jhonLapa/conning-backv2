using System.Text.Json.Serialization;

namespace Domain
{
    public class Planilla
    {
        public int IdPlanilla { get; set; }
        public int IdProyecto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public DateTime? PeriodoInicio { get; set; }  // ✅ NULL permitido
        public DateTime? PeriodoFin { get; set; }     // ✅ NULL permitido
        public DateTime? FechaPago { get; set; }      // ✅ NULL permitido
        public int Estado { get; set; }               // ❌ NOT NULL
        public DateTime FechaCreacion { get; set; }   // ❌ NOT NULL
        public string? UsuarioCreacion { get; set; }  // ✅ NULL permitido

        [JsonIgnore]
        public Proyecto? Proyecto { get; set; }

        [JsonIgnore]
        public ICollection<AportesPlanilla> AportesPlanilla { get; set; } = new List<AportesPlanilla>();
    }
}

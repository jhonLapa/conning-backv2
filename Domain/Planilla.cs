using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Planilla
    {
        public int IdPlanilla { get; set; }
        public int IdProyecto { get; set; }
        public string? Mes { get; set; }
        public DateTime? PeriodoInicio { get; set; } 
        public DateTime? PeriodoFin { get; set; }    
        public DateTime? FechaPago { get; set; }     
        public int Estado { get; set; }               
        public DateTime FechaCreacion { get; set; }   
        public string? UsuarioCreacion { get; set; } 
        public string? FrecuenciaPago { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? PeriodoTexto { get; private set; }
        public decimal TotalGeneral { get; set; }


        [JsonIgnore]
        public Proyecto? Proyecto { get; set; }

        [JsonIgnore]
        public ICollection<AportesPlanilla> AportesPlanilla { get; set; } = new List<AportesPlanilla>();

        [JsonIgnore]
        public ICollection<DetallePlanilla> Detalles { get; set; } = new List<DetallePlanilla>();

    }
}

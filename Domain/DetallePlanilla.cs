using System.Text.Json.Serialization;

namespace Domain
{
    public class DetallePlanilla
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


        public decimal? Horas60 { get; set; }
        public decimal? Horas100 { get; set; }
        public decimal? Indemnizacion { get; set; }

        // ==================================================
        // 🔹 Relaciones
        // ==================================================
        [JsonIgnore]
        public Planilla? Planilla { get; set; }

        [JsonIgnore]
        public TrabajadorProyecto? TrabajadorProyecto { get; set; }

    }
}

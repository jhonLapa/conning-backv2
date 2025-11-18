using System;

namespace Domain.Entities
{
    public class HistorialTrabajadorProyecto
    {
        public int IdHistorialTrabajadorProyecto { get; set; }
        public int IdTrabajadorProyecto { get; set; }
        public int IdCategoria { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? SueldoBase { get; set; }
        public string? Observacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones
        public TrabajadorProyecto TrabajadorProyecto { get; set; } = null!;
        public Categoria Categoria { get; set; } = null!;

        public ICollection<DetallePlanilla> DetallesPlanilla { get; set; } = new HashSet<DetallePlanilla>();


    }
}

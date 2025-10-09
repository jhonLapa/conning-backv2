namespace Domain
{
    public class TrabajadorProyecto
    {
        public int IdTrabajadorProyecto { get; set; }
        public int IdTrabajador { get; set; }
        public int IdProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // Relaciones
        public Trabajador Trabajador { get; set; }
        public Proyecto Proyecto { get; set; }
        public ICollection<DetallePlanilla> Detalles { get; set; } = new List<DetallePlanilla>();

    }
}

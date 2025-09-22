namespace Domain
{
    public class ProjectEmployee
    {
        public int IdProyectoTrabajador { get; set; }
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
        public string RolProyecto { get; set; } = null!;
        public int IdCategoria { get; set; }
        public string TipoSalario { get; set; } = null!;
        public decimal? MontoPersonalizado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relaciones
        public Project Project { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}

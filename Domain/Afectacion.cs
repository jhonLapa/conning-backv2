namespace Domain
{
    public class Afectacion
    {
        public int IdAfectacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }

        // 🔗 Relaciones
        public ICollection<ConfigAfectacion> ConfigAfectaciones { get; set; } = new HashSet<ConfigAfectacion>();

    }
}

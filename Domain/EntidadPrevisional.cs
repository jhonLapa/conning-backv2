namespace Domain
{
    public class EntidadPrevisional
    {
        public int IdEntidad { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public bool Estado { get; set; }

        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación: una Entidad tiene muchas comisiones
        public ICollection<EntidadComision> Comisiones { get; set; } = new List<EntidadComision>();
    }
}

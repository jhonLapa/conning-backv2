namespace Domain
{
    public class GrupoConcepto
    {
        public int IdGrupo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación inversa
        public ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();
    }
}

namespace Domain
{
    public class AFP
    {
        public int IdAFP { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación inversa
        public ICollection<AFPComision> AFPComision { get; set; } = new List<AFPComision>();
    }
}

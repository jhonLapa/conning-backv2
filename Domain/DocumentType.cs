namespace Domain
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Navegación inversa
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<Provider> Providers { get; set; } = new List<Provider>();
    }
}

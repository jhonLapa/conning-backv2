namespace Domain
{
    public class Provider
    {
        public int IdProveedor { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación
        public DocumentType DocumentType { get; set; } = null!;
    }
}

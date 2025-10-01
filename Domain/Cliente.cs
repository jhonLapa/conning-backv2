namespace Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // 🔗 Relaciones
        public TipoDocumento TipoDocumento { get; set; } = null!;
    }
}

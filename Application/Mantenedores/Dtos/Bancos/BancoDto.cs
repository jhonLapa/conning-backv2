namespace Application.Mantenedores.Dtos.Bancos
{
    public class BancoDto
    {
        public int IdBanco { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string SwiftCode { get; set; }
        public string CodigoPais { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

    }
}

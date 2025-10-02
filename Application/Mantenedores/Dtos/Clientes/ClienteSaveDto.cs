namespace Application.Mantenedores.Dtos.Clientes
{
    public class ClienteSaveDto
    {
        public string NombreCompleto { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}

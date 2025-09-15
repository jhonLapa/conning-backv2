using Domain;

namespace Application.Usuarios.Dto
{
    public class UserSaveDto
    {
        public string? NombreCompleto { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? NroDocumento { get; set; }
        public string? Biografia { get; set; }
        public string? Photo { get; set; }
        public int? IdEscuela { get; set; }

    }
}

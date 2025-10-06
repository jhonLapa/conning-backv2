using Domain;

namespace Application.UserRoles.Dto
{
    public class UserRoleDto
    {

        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }   // 👈 Agregar esto
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public User Users { get; set; } = null!;
        public Rol Roles { get; set; } = null!;

    }
}

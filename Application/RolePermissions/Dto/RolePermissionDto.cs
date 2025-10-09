using Domain;

namespace Application.RolePermissions.Dto
{
    public class RolePermissionDto
    {

        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }   // 👈 Agregar esto
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public Permission Permissions { get; set; } = null!;
        public Rol Roles { get; set; } = null!;

    }
}

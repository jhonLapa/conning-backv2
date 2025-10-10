using Domain;

namespace Application.RolePermissions.Dto
{
    public class RolePermissionDto
    {

        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int State { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public string? AuditCreateUser { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public Permission Permissions { get; set; } = null!;
        public Rol Roles { get; set; } = null!;

    }
}

using Domain;

namespace Application.MenuRoles.Dto
{
    public class MenuRoleDto
    {
        public int MenuRoleId { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public int State { get; set; }
        public DateTime FechaCreacion { get; set; }   
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public Menu Menus { get; set; } = null!;
        public Rol Roles { get; set; } = null!;

    }
}

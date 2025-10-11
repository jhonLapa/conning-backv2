using Application.Mantenedores.Dtos.Menus;
using Domain;

namespace Application.Permissions.Dto
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }
        public int MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int State { get; set; }

        // Fechas 
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public MenuDto Menu { get; set; } = null!;


    }
}

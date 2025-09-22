
using Application.Mantenedores.Dtos.Roles;
using Domain;

namespace Application.Usuarios.Dto
{
    public class UserRolSaveDto 
    {
        public UserSaveDto User { get; set; }
        public RoleDto Rol { get; set; }
    }
}

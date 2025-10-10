namespace Domain
{
    public class RolePermission : BaseDomain
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public char State { get; set; }
        public virtual Permission? Permissions { get; set; }
        public virtual Rol? Roles { get; set; }
    }
}

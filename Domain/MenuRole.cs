namespace Domain
{
    public class MenuRole : BaseDomain
    {
        public int MenuRoleId { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public bool? State { get; set; }
        public virtual Menu? Menus { get; set; }
        public virtual Rol? Roles { get; set; }
    }
}

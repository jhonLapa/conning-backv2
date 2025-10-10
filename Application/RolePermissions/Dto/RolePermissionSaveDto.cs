namespace Application.RolePermissions.Dto
{
    public class RolePermissionSaveDto
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public char State { get; set; }
    }
}   

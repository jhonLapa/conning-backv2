namespace Application.Permissions.Dto
{
    public class PermissionSelectDto
    {
        public int MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}

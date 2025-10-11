namespace Domain
{
    public class Permission : BaseDomain
    {
        public int PermissionId { get; set; }
        public int MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int State { get; set; }

        // 🔗 Relaciones
        public Menu Menu { get; set; } = null!;
    }
}

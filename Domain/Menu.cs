namespace Domain
{
    public class Menu : BaseDomain
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int? FatherId { get; set; } 
        public int State { get; set; }
        public int Position { get; set; }

        // 🔗 Relaciones
        public virtual ICollection<Permission>? Permissions { get; set; }

    }
}
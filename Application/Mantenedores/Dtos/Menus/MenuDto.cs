namespace Application.Mantenedores.Dtos.Menus
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int? FatherId { get; set; }
        public int? State { get; set; }
        public int Position { get; set; }

    }
}

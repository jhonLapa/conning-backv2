namespace Application.Mantenedores.Dtos.Menus
{
    public class MenuSaveDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int? FatherId { get; set; }
        public int Position { get; set; } 
    }
}
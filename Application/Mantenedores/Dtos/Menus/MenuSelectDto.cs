namespace Application.Mantenedores.Dtos.Menus
{
    public class MenuSelectDto
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int? FatherId { get; set; }
    }
}
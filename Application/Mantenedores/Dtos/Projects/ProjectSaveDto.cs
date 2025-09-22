namespace Application.Mantenedores.Dtos.Projects
{
    public class ProjectSaveDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool State { get; set; }
    }
}

namespace Infraestructure.Core.Dtos
{
    public class ProyectoPlanillaTotalDto
    {
        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal TotalPlanillas { get; set; }
    }
}

namespace Application.Mantenedores.Dtos.Proyectos
{
    public class ProyectoSaveDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public string? FrecuenciaPago { get; set; }
    }
}

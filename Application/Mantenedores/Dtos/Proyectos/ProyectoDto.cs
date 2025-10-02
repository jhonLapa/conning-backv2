using Domain;

namespace Application.Mantenedores.Dtos.Proyectos
{
    public class ProyectoDto
    {
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public string? FrecuenciaPago { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public Cliente Cliente { get; set; } = null!;

    }
}

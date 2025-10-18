using Domain;

namespace Application.ProyectoEncargados.Dto
{
    public class ProyectoEncargadoDto
    {
        public int IdProyectoEncargado { get; set; }
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
        public string rol { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Estado { get; set; }

        // 🔗 Relaciones
        public Trabajador Trabajador { get; set; } = null!;
    }
}

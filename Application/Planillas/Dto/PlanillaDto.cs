using Application.Mantenedores.Dtos.Proyectos;

namespace Application.Planillas.Dto
{
    public class PlanillaDto
    {
        public int IdPlanilla { get; set; }
        public int IdProyecto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? FrecuenciaPago { get; set; }
        public string? PeriodoTexto { get; set; }
        public ProyectoDto? Proyecto { get; set; }
    }
}

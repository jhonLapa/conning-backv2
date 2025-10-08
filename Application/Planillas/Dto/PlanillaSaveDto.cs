namespace Application.Planillas.Dto
{
    public class PlanillaSaveDto
    {
        public int IdProyecto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public DateTime FechaPago { get; set; }
    }
}

namespace Application.AportesPlanillas.Dto
{
    public class AportesPlanillaSaveDto
    {
        public int IdPlanilla { get; set; }
        public string TipoAporte { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }
    }
}

using Application.Planillas.Dto;
using Domain;

namespace Application.AportesPlanillas.Dto
{
    public class AportesPlanillaDto
    {
        public int IdAportePlanilla { get; set; }
        public int IdPlanilla { get; set; }
        public string TipoAporte { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }

    }
}

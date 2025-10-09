using Domain;

namespace Application.DetallePlanillas.Dto
{
    public class DetallePlanillaDto
    {

        public int IdDetallePlanilla { get; set; }
        public int IdPlanilla { get; set; }
        public int IdTrabajadorProyecto { get; set; }

        public int DiasTrabajados { get; set; }
        public int HorasTrabajadas { get; set; }
        public decimal PrimeraQuincena { get; set; }
        public decimal SegundaQuincena { get; set; }
        public decimal TotalMensual { get; set; }
        public decimal TotalHoras { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}

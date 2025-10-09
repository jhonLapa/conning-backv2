using Domain;

namespace Application.DetallePlanillas.Dto
{
    public class DetallePlanillaSaveDto
    {

        public int IdPlanilla { get; set; }
        public int IdTrabajadorProyecto { get; set; }
        public int Cantidad { get; set; }
        public int DiasTrabajados { get; set; }
        public int HorasTrabajadas { get; set; }
        public int PrimeraQuincena { get; set; }
        public int SegundaQuincena { get; set; }
        public decimal TotalMensual { get; set; }
        public int TotalHoras { get; set; }
    }
}

namespace Application.DetallePlanillas.Dto
{
    public class DetallePlanillaSaveDto
    {

        public int IdPlanilla { get; set; }
        public int? IdTrabajadorProyecto { get; set; } // antes int
        public decimal? TotalDescuentos { get; set; } // antes int
        public int DiasTrabajados { get; set; }
        public int HorasTrabajadas { get; set; }
        public decimal TotalMonto { get; set; }
        public decimal TotalHoras { get; set; }

    }
}

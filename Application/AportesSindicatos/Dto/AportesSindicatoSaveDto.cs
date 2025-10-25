namespace Application.AportesSindicatos.Dto
{
    public class AportesSindicatoSaveDto
    {
        public int IdProyecto { get; set; }
        public string? Mes { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public string Observacion { get; set; }
    }
}

namespace Application.PagoVentaCreditos.Dto
{
    public class PagoVentaCreditoSaveDto
    {
        public int IdPagoVentaCredito { get; set; }
        public int IdVenta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string Observacion { get; set; }
    }
}

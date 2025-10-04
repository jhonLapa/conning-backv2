namespace Application.PagoCompraCreditos.Dto
{
    public class PagoCompraCreditoSaveDto
    {
        public int IdPagoCompraCredito { get; set; }
        public int IdCompra { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string Observacion { get; set; }
    }
}

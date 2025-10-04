namespace Application.PagoCompraCreditos.Dto
{
    public class PagoCompraCreditoDto
    {
        public int IdPagoCompraCredito { get; set; }
        public int IdCompra { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string EstadoPago { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // Relaciones
        public Domain.Compra Compra { get; set; }
    }
}

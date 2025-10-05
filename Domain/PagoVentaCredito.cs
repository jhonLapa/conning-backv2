namespace Domain
{
    public class PagoVentaCredito
    {
        public int IdPagoVentaCredito { get; set; }
        public int IdVenta { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string EstadoPago { get; set; } = "PENDIENTE";  // valor por defecto
        public string? Observacion { get; set; }               // puede ser NULL
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // NOT NULL con valor por defecto

        // Relaciones
        public Venta Venta { get; set; }
    }
}

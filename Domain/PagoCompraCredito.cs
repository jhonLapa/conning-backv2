using System.Text.Json.Serialization;

namespace Domain
{
    public class PagoCompraCredito
    {
        public int IdPagoCompraCredito { get; set; }
        public int IdCompra { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public string EstadoPago { get; set; }= "PENDIENTE";  // valor por defecto
        public string? Observacion { get; set; }             // puede ser NULL
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // NOT NULL con valor por defecto
        public string? UsuarioCreacion { get; set; }

        // Relaciones
        public Compra Compra { get; set; }
    }
}

using Domain;

namespace Application.Ventas.Dto
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public int IdCliente { get; set; }
        public string? FormaPago { get; set; }
        public string TipoMoneda { get; set; } = null!;
        public string? Observacion { get; set; }

        // Totales
        public decimal SubTotal { get; set; }
        public decimal Anticipos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal Isc { get; set; }
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }
        public decimal OtrosCargos { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal MontoRedondeo { get; set; }
        public decimal ImporteTotal { get; set; }

        // Detracción
        public bool DetraccionAplica { get; set; }
        public decimal DetraccionPorcentaje { get; set; }
        public decimal DetraccionMonto { get; set; }
        public string? CuentaDetraccion { get; set; }

        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones
        public Cliente Cliente { get; set; } = null!;
        public TipoComprobante TipoComprobante { get; set; } = null!;
    }
}

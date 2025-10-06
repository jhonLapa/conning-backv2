using Domain;

namespace Application.Compras.Dto
{
    public class CompraSaveDto
    {

        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public DateTime? FechaEmision { get; set; }
        public int IdProveedor { get; set; }
        public string? FormaPago { get; set; } = null!;
        public string TipoMoneda { get; set; } = null!;
        public string? Observacion { get; set; } = null!;

        // Totales
        public decimal SubTotal { get; set; }
        public decimal Anticipios { get; set; }
        public decimal Descuentos { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal Isc { get; set; }
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }
        public decimal OtrosCargos { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal MontoRedondeo { get; set; }
        public decimal ImproteTotal { get; set; }
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}

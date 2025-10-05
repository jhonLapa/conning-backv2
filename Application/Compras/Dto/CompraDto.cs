using Application.DetalleCompras.Dto;
using Application.PagoCompraCreditos.Dto;
using Application.Mantenedores.Dtos.Proveedores;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Domain;

namespace Application.Compras.Dto
{
    public class CompraDto
    {

        public int IdCompra { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public int IdProveedor { get; set; }
        public string? FormaPago { get; set; } = null!;
        public string TipoMoneda { get; set; } = null!;
        public string Observacion { get; set; } = null!;

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

        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones (solo lo que necesitas mostrar)
        public ProveedorDto Proveedor { get; set; } = null!;
        public TipoComprobanteDto TipoComprobante { get; set; } = null!;

        // 🔗 Nuevos
        public List<DetalleCompraDto> Detalles { get; set; } = new();
        public List<PagoCompraCreditoDto> PagosCredito { get; set; } = new();
    }
}

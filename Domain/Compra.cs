namespace Domain
{

    public class Compra
    {
        public int IdCompra { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int IdProveedor { get; set; }
        public string FormaPago { get; set; }
        public string TipoMoneda { get; set; }
        public string Observacion { get; set; }

        // Totales
        public decimal SubTotal { get; set; }
        public decimal Anticipos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal Isc { get; set; }
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }
        public decimal OtrosCargos { get; set; }
        public decimal OtrosTributos { get; set; }
        public decimal MontoRedondeo { get; set; }
        public decimal ImporteTotal { get; set; }


        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // Relaciones
        public Proveedor Proveedor { get; set; } = null!;
        public TipoComprobante TipoComprobante { get; set; } = null!;
        public ICollection<DetalleCompra> Detalles { get; set; }
        public ICollection<PagoCompraCredito> PagosCredito { get; set; }
    }
}

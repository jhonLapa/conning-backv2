namespace Domain
{

    public class Compra
    {
        public int IdCompra { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public int IdProveedor { get; set; }
        public string FormaPago { get; set; }
        public string TipoMoneda { get; set; }
        public string Observacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        // Relaciones
        public Proveedor Proveedor { get; set; }
        public TipoComprobante TipoComprobante { get; set; }
        public ICollection<DetalleCompra> Detalles { get; set; }
    }
}

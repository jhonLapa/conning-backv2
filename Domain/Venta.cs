namespace Domain
{

    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public int IdCliente { get; set; }
        public string FormaPago { get; set; }
        public string TipoMoneda { get; set; }
        public string Observacion { get; set; }
        public decimal ImporteTotal { get; set; }

        // Relaciones
        public Cliente Cliente { get; set; }
        public TipoComprobante TipoComprobante { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; }
        public ICollection<PagoVentaCredito> PagosCredito { get; set; }
    }
}

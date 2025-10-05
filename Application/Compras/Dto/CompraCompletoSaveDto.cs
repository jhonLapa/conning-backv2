namespace Application.Compras.Dto
{
    public class CompraCompletoSaveDto
    {
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public DateTime? FechaEmision { get; set; }
        public int IdProveedor { get; set; }
        public string FormaPago { get; set; } = null!; // "Contado" o "Credito"
        public string TipoMoneda { get; set; } = null!;
        public string? Observacion { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Descuentos { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal Igv { get; set; }
        public decimal ImporteTotal { get; set; }

        // Relacionados
        public List<DetallesCompraSaveDto> Detalles { get; set; } = new();
        public List<PagosCompraCreditoSaveDto>? PagosCredito { get; set; } // solo si es crédito
    }

    public class DetallesCompraSaveDto
    {
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class PagosCompraCreditoSaveDto
    {
        public DateTime? FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
    }
}
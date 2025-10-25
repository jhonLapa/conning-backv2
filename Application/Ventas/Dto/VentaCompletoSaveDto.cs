using Domain.Entities;

namespace Application.Ventas.Dto
{
    public class VentaCompletoSaveDto
    {
        public int IdVenta { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public DateTime? FechaEmision { get; set; }
        public int IdCliente { get; set; }
        public string FormaPago { get; set; } = null!; // "Contado" o "Credito"
        public string TipoMoneda { get; set; } = null!;
        public string? Observacion { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Descuentos { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Igv { get; set; }
        public decimal ImporteTotal { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public int IdProyecto { get; set; }
        public int Estado { get; set; }


        // Relacionados
        public List<DetallesVentaSaveDto> Detalles { get; set; } = new();
        public List<PagosVentaCreditoSaveDto>? PagosCredito { get; set; } 
        public List<DepositosVentaSaveDto>? DepositosVenta { get; set; }
    }

    public class DetallesVentaSaveDto
    {
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class PagosVentaCreditoSaveDto
    {
        public DateTime? FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
    }

    public class DepositosVentaSaveDto
    {
        public DateTime FechaDeposito { get; set; }
        public decimal Monto { get; set; }
    }
}
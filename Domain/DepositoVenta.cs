using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class DepositoVenta
    {
        public int IdDepositoVenta { get; set; }
        public int IdVenta { get; set; }
        public DateTime FechaDeposito { get; set; }
        public decimal Monto { get; set; }
        public string? Banco { get; set; }
        public string? NumeroOperacion { get; set; }
        public string? Observacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }

        [JsonIgnore]
        public Venta Venta { get; set; } = null!;
    }
}

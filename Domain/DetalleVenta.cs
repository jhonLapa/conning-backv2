using System.Text.Json.Serialization;

namespace Domain
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? Icbper { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relaciones
        [JsonIgnore] // 👈 rompe el loop
        public Venta Venta { get; set; }
    }
}

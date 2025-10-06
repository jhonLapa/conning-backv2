using System.Text.Json.Serialization;

namespace Domain
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? Icbper { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime FechaCreacion { get; set; }
        // Relaciones
        [JsonIgnore] // 👈 rompe el loop
        public Compra Compra { get; set; }
    }
}

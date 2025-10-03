using Domain;

namespace Application.DetalleCompras.Dto
{
    public class DetalleCompraDto
    {

        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? Icbper { get; set; }
        public decimal? ValorTotal { get; set; }

        public Compra Compra { get; set; } = null!;
    }
}

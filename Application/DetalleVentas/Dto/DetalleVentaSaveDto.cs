namespace Application.DetalleVentas.Dto
{
    public class DetalleVentaSaveDto
    {
        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? Icbper { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}

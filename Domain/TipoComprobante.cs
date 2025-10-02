namespace Domain
{
    public class TipoComprobante
    {
        public int IdTipoComprobante { get; set; }
        public string Codigo { get; set; }      // hasta 10
        public string Nombre { get; set; }      // hasta 100
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // Relaciones
        public ICollection<Venta> Ventas { get; set; }
    }
}
